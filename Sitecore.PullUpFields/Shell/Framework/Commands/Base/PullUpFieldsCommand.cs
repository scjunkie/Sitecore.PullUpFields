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

using System.Collections.Specialized;
using System.Linq;

using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

using Sitecore.PullUpFields.Configuration;
using Sitecore.PullUpFields.Configuration.Base;

namespace Sitecore.PullUpFields.Shell.Framework.Commands.Base
{
    public abstract class PullUpFieldsCommand : Command
    {
        protected static readonly IPullUpFieldsSettings PullUpFieldsSettings = CreateNewPullUpFieldsSettings();

        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            Context.ClientPage.Start(GetPullUpFieldsClientPipelineName(), CreateNewParameters(GetItem(context)));
        }

        protected abstract string GetPullUpFieldsClientPipelineName();

        private NameValueCollection CreateNewParameters(Item templateItem)
        {
            return new NameValueCollection 
            {
                {"icon", GetIcon()},
                {"database", templateItem.Database.Name},
                {"templateId", templateItem.ID.ToString()}
            };
        }

        protected abstract string GetIcon();

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (ShouldEnable(GetItem(context)))
            {
                return CommandState.Enabled;
            }

            return CommandState.Hidden;
        }

        protected static Item GetItem(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            Assert.ArgumentNotNull(context.Items, "context.Items");
            Assert.ArgumentCondition(context.Items.Any(), "context.Items", "At least one item must be present!");
            return context.Items.FirstOrDefault();
        }

        private static bool ShouldEnable(Item item)
        {
            return item != null 
                    && IsTemplate(item) 
                    && HasTemplateFields(item);
        }

        private static bool IsTemplate(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return TemplateManager.IsTemplate(item);
        }

        private static bool HasTemplateFields(Item item)
        {
            TemplateItem templateItem = item;
            return templateItem != null && templateItem.OwnFields.Any();
        }

        private static IPullUpFieldsSettings CreateNewPullUpFieldsSettings()
        {
            ISettingsFactory settingsFactory = SettingsFactory.CreateNewSettingsFactory();
            return settingsFactory.CreateNewPullUpFieldsSettings();
        }
    }
}