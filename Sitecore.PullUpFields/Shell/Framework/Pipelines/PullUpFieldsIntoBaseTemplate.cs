/**************************************************************************
 * Copyright 2013 Michael Reynolds
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Shell.Framework;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

using Sitecore.PullUpFields.Configuration;
using Sitecore.PullUpFields.Configuration.Base;
using Sitecore.PullUpFields.Entities;

namespace Sitecore.PullUpFields.Shell.Framework.Pipelines
{
    public class PullUpFieldsIntoBaseTemplate
    {
        private static readonly IPullUpFieldsSettings PullUpFieldsSettings = CreateNewPullUpFieldsSettings();
        
        public void EnsureTemplate(ClientPipelineArgs args)
        {
            Item item = GetItem(args);
            if (!IsTemplate(item))
            {
                CancelOperation(args, PullUpFieldsSettings.NotTemplateMessage);
            }
        }

        private static bool IsTemplate(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return TemplateManager.IsTemplate(item);
        }

        public void SelectFields(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!args.IsPostBack)
            {
                UrlString urlString = new UrlString(UIUtil.GetUri("control:TreeListExEditor"));
                UrlHandle urlHandle = new UrlHandle();
                urlHandle["title"] = PullUpFieldsSettings.SelectFieldsDialogTitle;
                urlHandle["text"] = PullUpFieldsSettings.SelectFieldsDialogText;
                urlHandle["source"] = GetSelectFieldsDialogSource(args);
                urlHandle.Add(urlString);
                SheerResponse.ShowModalDialog
                (
                    urlString.ToString(), 
                    PullUpFieldsSettings.SelectFieldsDialogWidth, 
                    PullUpFieldsSettings.SelectFieldsDialogHeight, 
                    string.Empty, 
                    true
                );
                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                args.Parameters["fieldIds"] = args.Result;
                args.IsPostBack = false;
            }
            else
            {
                CancelOperation(args);
            }
        }

        private static string GetSelectFieldsDialogSource(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["database"], "args.Parameters[\"database\"]");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["templateId"], "args.Parameters[\"templateId\"]");
            return string.Format
            (
                "AllowMultipleSelection=true&DatabaseName={0}&DataSource={1}&IncludeTemplatesForSelection=Template field",
                args.Parameters["database"],
                args.Parameters["templateId"]
            );
        }

        public void ChooseBaseTemplateName(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!args.IsPostBack)
            {
                SheerResponse.Input(PullUpFieldsSettings.ChooseBaseTemplateNamePrompt, PullUpFieldsSettings.DefaultChooseBaseTemplateName);
                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                args.Parameters["baseTemplateName"] = args.Result;
                args.IsPostBack = false;
            }
            else
            {
                CancelOperation(args);
            }
        }

        public void ChooseBaseTemplateLocation(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!args.IsPostBack)
            {
                Dialogs.BrowseItem
                (
                    PullUpFieldsSettings.SelectBaseTemplateLocationDialogTitle,
                    PullUpFieldsSettings.SelectBaseTemplateLocationDialogText,
                    args.Parameters["icon"],
                    PullUpFieldsSettings.SelectBaseTemplateLocationDialogOKButtonText,
                    PullUpFieldsSettings.SelectBaseTemplateLocationSaveRoot,
                    args.Parameters["templateId"]
                );

                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                args.Parameters["baseTemplateParentId"] = args.Result;
                args.IsPostBack = false;
            }
            else
            {
                CancelOperation(args);
            }
        }

        public void CreateNewBaseTemplate(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Database database = GetDatabase(args.Parameters["database"]);
            Item baseTemplateParent = database.GetItem(args.Parameters["baseTemplateParentId"]);
            Item baseTemplate = baseTemplateParent.Add(args.Parameters["baseTemplateName"], new TemplateID(TemplateIDs.Template));
            SetBaseTemplateField(baseTemplate, TemplateIDs.StandardTemplate);
            args.Parameters["baseTemplateId"] = baseTemplate.ID.ToString();
        }

        public void ChooseExistingBaseTemplate(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!args.IsPostBack)
            {
                ItemListerOptions itemListerOptions = new ItemListerOptions
                {
                    ButtonText = PullUpFieldsSettings.ChooseExistingBaseTemplateDialogOKButtonText,
                    Icon = args.Parameters["icon"],
                    Title = PullUpFieldsSettings.ChooseExistingBaseTemplateDialogTitle,
                    Text = PullUpFieldsSettings.ChooseExistingBaseTemplateDialogText
                };

                TemplateItem templateItem = GetItem(args);
                itemListerOptions.Items = ExcludeStandardTemplate(templateItem.BaseTemplates).Select(baseTemplate => baseTemplate.InnerItem).ToList();
                itemListerOptions.AddTemplate(TemplateIDs.Template);
                SheerResponse.ShowModalDialog(itemListerOptions.ToUrlString().ToString(), true);
                args.WaitForPostBack();
            }
            else if (args.HasResult)
            {
                args.Parameters["baseTemplateId"] = args.Result;
                args.IsPostBack = false;
            }
            else
            {
                CancelOperation(args);
            }
        }

        private static IEnumerable<TemplateItem> ExcludeStandardTemplate(IEnumerable<TemplateItem> baseTemplates)
        {
            if (baseTemplates != null && baseTemplates.Any())
            {
                return baseTemplates.Where(baseTemplate => baseTemplate.ID != TemplateIDs.StandardTemplate);
            }

            return baseTemplates;
        }

        public void EnsureCanCreateAtBaseTemplateLocation(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Item baseTemplateParent = GetItem(args.Parameters["database"], args.Parameters["baseTemplateParentId"]);
            if (!baseTemplateParent.Access.CanCreate())
            {
                CancelOperation(args, PullUpFieldsSettings.CannotCreateItemAtSelectedLocationMessage);
            }
        }

        public void Execute(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["database"], "args.Parameters[\"database\"]");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["baseTemplateId"], "args.Parameters[\"baseTemplateId\"]");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["fieldIds"], "args.Parameters[\"fieldIds\"]");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["templateId"], "args.Parameters[\"templateId\"]");
            Database database = GetDatabase(args.Parameters["database"]);
            Item baseTemplate = database.GetItem(args.Parameters["baseTemplateId"]);
            SetBaseTemplateField(baseTemplate, TemplateIDs.StandardTemplate);
            IDictionary<SectionInformation, IList<Item>> sectionsWithFields = GetSectionsWithFields(database, args.Parameters["fieldIds"]);

            foreach (SectionInformation sectionInformation in sectionsWithFields.Keys)
            {
                IEnumerable<Item> fields = sectionsWithFields[sectionInformation];
                MoveFieldsToAppropriateSection(baseTemplate, sectionInformation, fields);
            }

            Item templateItem = database.GetItem(args.Parameters["templateId"]);
            ListString baseTemplateIDs = new ListString(templateItem[PullUpFieldsSettings.BaseTemplateFieldName], PullUpFieldsSettings.IDDelimiter);
            baseTemplateIDs.Add(baseTemplate.ID.ToString());
            SetBaseTemplateField(templateItem, baseTemplateIDs);
            Refresh(templateItem);
        }

        private static void MoveFieldsToAppropriateSection(Item baseTemplate, SectionInformation sectionInformation, IEnumerable<Item> fields)
        {
            TemplateItem templateItem = baseTemplate;
            TemplateSectionItem templateSectionItem = templateItem.GetSection(sectionInformation.Section.Name);

            if (templateSectionItem != null)
            {
                MoveFields(templateSectionItem, fields);
            }
            else
            {
                Item sectionItemCopy = sectionInformation.Section.CopyTo(baseTemplate, sectionInformation.Section.Name, ID.NewID, false);
                MoveFields(sectionItemCopy, fields);
            }

            if (!sectionInformation.Section.GetChildren().Any())
            {
                sectionInformation.Section.Delete();
            }
        }

        private static void MoveFields(TemplateSectionItem templateSectionItem, IEnumerable<Item> fields)
        {
            Assert.ArgumentNotNull(templateSectionItem, "templateSectionItem");
            Assert.ArgumentNotNull(fields, "fields");
            foreach (Item field in fields)
            {
                field.MoveTo(templateSectionItem.InnerItem);
            }
        }

        private static void SetBaseTemplateField(Item templateItem, object baseTemplateIds)
        {
            Assert.ArgumentNotNull(templateItem, "templateItem");
            templateItem.Editing.BeginEdit();
            templateItem[PullUpFieldsSettings.BaseTemplateFieldName] = EnsureDistinct(baseTemplateIds.ToString(), PullUpFieldsSettings.IDDelimiter);
            templateItem.Editing.EndEdit();
        }

        private static string EnsureDistinct(string strings, char delimiter)
        {
            return string.Join(delimiter.ToString(), EnsureDistinct(strings.ToString().Split(new[] { delimiter })));
        }

        private static IEnumerable<string> EnsureDistinct(IEnumerable<string> strings)
        {
            return strings.Distinct();
        }

        private static IDictionary<SectionInformation, IList<Item>> GetSectionsWithFields(Database database, string fieldIds)
        {
            Assert.ArgumentNotNull(database, "database");
            Assert.ArgumentNotNull(fieldIds, "fieldIds");
            IDictionary<SectionInformation, IList<Item>> sectionsWithFields = new Dictionary<SectionInformation, IList<Item>>();

            foreach (TemplateFieldItem field in GetFields(database, fieldIds))
            {
                SectionInformation sectionInformation = new SectionInformation(field.Section.InnerItem);
                if (sectionsWithFields.ContainsKey(sectionInformation))
                {
                    sectionsWithFields[sectionInformation].Add(field.InnerItem);
                }
                else
                {
                    sectionsWithFields.Add(sectionInformation, new List<Item>(new[] { field.InnerItem }));
                }
            }

            return sectionsWithFields;
        }

        private static IEnumerable<TemplateFieldItem> GetFields(Database database, string fieldIds)
        {
            Assert.ArgumentNotNull(database, "database");
            Assert.ArgumentNotNull(fieldIds, "fieldIds");
            ListString fieldIdsListString = new ListString(fieldIds, PullUpFieldsSettings.IDDelimiter);
            IList<TemplateFieldItem> fields = new List<TemplateFieldItem>();

            foreach (string fieldId in fieldIdsListString)
            {
                fields.Add(database.GetItem(fieldId));
            }

            return fields;
        }

        private static Item GetItem(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["database"], "args.Parameters[\"database\"]");
            Assert.ArgumentNotNullOrEmpty(args.Parameters["templateId"], "args.Parameters[\"templateId\"]");
            Item item = GetItem(args.Parameters["database"], args.Parameters["templateId"]);
            Assert.IsNotNull(item, "Item cannot be null!");
            return item;
        }

        private static Item GetItem(string databaseName, string itemId)
        {
            Assert.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            Assert.ArgumentNotNullOrEmpty(itemId, "itemId");
            ID id = GetID(itemId);
            Assert.IsTrue(!ID.IsNullOrEmpty(id), "itemId is not valid!");
            Database database = GetDatabase(databaseName);
            Assert.IsNotNull(database, "Database is not valid!");
            return database.GetItem(id);
        }

        private static ID GetID(string itemId)
        {
            ID id;
            if (ID.TryParse(itemId, out id))
            {
                return id;
            }

            return ID.Null;
        }

        private static Database GetDatabase(string databaseName)
        {
            Assert.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            return Factory.GetDatabase(databaseName);
        }
        
        private static void CancelOperation(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            CancelOperation(args, "Operation has been cancelled!");
        }

        private static void CancelOperation(ClientPipelineArgs args, string alertMessage)
        {
            Assert.ArgumentNotNull(args, "args");
            if (!string.IsNullOrEmpty(alertMessage))
            {
                SheerResponse.Alert(alertMessage);
            }

            args.AbortPipeline();
        }

        private static void Refresh(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            Context.ClientPage.ClientResponse.Timer(string.Format("item:load(id={0})", item.ID), 1);
        }

        private static IPullUpFieldsSettings CreateNewPullUpFieldsSettings()
        {
            ISettingsFactory settingsFactory = SettingsFactory.CreateNewSettingsFactory();
            return settingsFactory.CreateNewPullUpFieldsSettings();
        }
    }
}
