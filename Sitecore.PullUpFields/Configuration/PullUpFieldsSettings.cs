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
using System.Web;

using Sitecore.Diagnostics;

using Sitecore.PullUpFields.Configuration.Base;

namespace Sitecore.PullUpFields.Configuration
{
    public class PullUpFieldsSettings : IPullUpFieldsSettings
    {
        private ISettings Settings { get; set; }

        public char IDDelimiter
        {
            get
            {
                return '|';
            }
        }

        public string BaseTemplateFieldName
        {
            get
            {
                return "__Base template";
            }
        }

        private string _NotTemplateMessage;
        public string NotTemplateMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_NotTemplateMessage))
                {
                    _NotTemplateMessage = Settings.GetSetting("Sitecore.PullUpFields.NotTemplateMessage");
                }

                return _NotTemplateMessage;
            }
        }

        private string _SelectFieldsDialogTitle;
        public string SelectFieldsDialogTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectFieldsDialogTitle))
                {
                    _SelectFieldsDialogTitle = Settings.GetSetting("Sitecore.PullUpFields.SelectFieldsDialogTitle");
                }

                return _SelectFieldsDialogTitle;
            }
        }

        private string _SelectFieldsDialogText;
        public string SelectFieldsDialogText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectFieldsDialogText))
                {
                    _SelectFieldsDialogText = Settings.GetSetting("Sitecore.PullUpFields.SelectFieldsDialogText");
                }

                return _SelectFieldsDialogText;
            }
        }

        private string _SelectFieldsDialogWidth;
        public string SelectFieldsDialogWidth
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectFieldsDialogWidth))
                {
                    _SelectFieldsDialogWidth = Settings.GetSetting("Sitecore.PullUpFields.SelectFieldsDialogWidth");
                }

                return _SelectFieldsDialogWidth;
            }
        }

        private string _SelectFieldsDialogHeight;
        public string SelectFieldsDialogHeight
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectFieldsDialogHeight))
                {
                    _SelectFieldsDialogHeight = Settings.GetSetting("Sitecore.PullUpFields.SelectFieldsDialogHeight");
                }

                return _SelectFieldsDialogHeight;
            }
        }

        private string _ChooseBaseTemplateNamePrompt;
        public string ChooseBaseTemplateNamePrompt
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ChooseBaseTemplateNamePrompt))
                {
                    _ChooseBaseTemplateNamePrompt = Settings.GetSetting("Sitecore.PullUpFields.ChooseBaseTemplateNamePrompt");
                }

                return _ChooseBaseTemplateNamePrompt;
            }
        }

        private string _DefaultChooseBaseTemplateName;
        public string DefaultChooseBaseTemplateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_DefaultChooseBaseTemplateName))
                {
                    _DefaultChooseBaseTemplateName = Settings.GetSetting("Sitecore.PullUpFields.DefaultChooseBaseTemplateName");
                }

                return _DefaultChooseBaseTemplateName;
            }
        }

        private string _SelectBaseTemplateLocationDialogTitle;
        public string SelectBaseTemplateLocationDialogTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectBaseTemplateLocationDialogTitle))
                {
                    _SelectBaseTemplateLocationDialogTitle = Settings.GetSetting("Sitecore.PullUpFields.SelectBaseTemplateLocationDialogTitle");
                }

                return _SelectBaseTemplateLocationDialogTitle;
            }
        }

        private string _SelectBaseTemplateLocationDialogText;
        public string SelectBaseTemplateLocationDialogText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectBaseTemplateLocationDialogText))
                {
                    _SelectBaseTemplateLocationDialogText = Settings.GetSetting("Sitecore.PullUpFields.SelectBaseTemplateLocationDialogText");
                }

                return _SelectBaseTemplateLocationDialogText;
            }
        }

        private string _SelectBaseTemplateLocationDialogOKButtonText;
        public string SelectBaseTemplateLocationDialogOKButtonText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectBaseTemplateLocationDialogOKButtonText))
                {
                    _SelectBaseTemplateLocationDialogOKButtonText = Settings.GetSetting("Sitecore.PullUpFields.SelectBaseTemplateLocationDialogOKButtonText");
                }

                return _SelectBaseTemplateLocationDialogOKButtonText;
            }
        }

        private string _SelectBaseTemplateLocationSaveRoot;
        public string SelectBaseTemplateLocationSaveRoot
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_SelectBaseTemplateLocationSaveRoot))
                {
                    _SelectBaseTemplateLocationSaveRoot = Settings.GetSetting("Sitecore.PullUpFields.SelectBaseTemplateLocationSaveRoot");
                }

                return _SelectBaseTemplateLocationSaveRoot;
            }
        }

        private string _ChooseExistingBaseTemplateDialogTitle;
        public string ChooseExistingBaseTemplateDialogTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ChooseExistingBaseTemplateDialogTitle))
                {
                    _ChooseExistingBaseTemplateDialogTitle = Settings.GetSetting("Sitecore.PullUpFields.ChooseExistingBaseTemplateDialogTitle");
                }

                return _ChooseExistingBaseTemplateDialogTitle;
            }
        }

        private string _ChooseExistingBaseTemplateDialogText;
        public string ChooseExistingBaseTemplateDialogText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ChooseExistingBaseTemplateDialogText))
                {
                    _ChooseExistingBaseTemplateDialogText = Settings.GetSetting("Sitecore.PullUpFields.ChooseExistingBaseTemplateDialogText");
                }

                return _ChooseExistingBaseTemplateDialogText;
            }
        }

        private string _ChooseExistingBaseTemplateDialogOKButtonText;
        public string ChooseExistingBaseTemplateDialogOKButtonText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ChooseExistingBaseTemplateDialogOKButtonText))
                {
                    _ChooseExistingBaseTemplateDialogOKButtonText = Settings.GetSetting("Sitecore.PullUpFields.ChooseExistingBaseTemplateDialogOKButtonText");
                }

                return _ChooseExistingBaseTemplateDialogOKButtonText;
            }
        }

        private string _CannotCreateItemAtSelectedLocationMessage;
        public string CannotCreateItemAtSelectedLocationMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_CannotCreateItemAtSelectedLocationMessage))
                {
                    _CannotCreateItemAtSelectedLocationMessage = Settings.GetSetting("Sitecore.PullUpFields.CannotCreateItemAtSelectedLocationMessage");
                }

                return _CannotCreateItemAtSelectedLocationMessage;
            }
        }

        private string _OperationCancelledMessage;
        public string OperationCancelledMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_OperationCancelledMessage))
                {
                    _OperationCancelledMessage = Settings.GetSetting("Sitecore.PullUpFields.OperationCancelledMessage");
                }

                return _OperationCancelledMessage;
            }
        }

        private string _PullUpFieldsIntoNewBaseTemplateCommandIcon;
        public string PullUpFieldsIntoNewBaseTemplateCommandIcon
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PullUpFieldsIntoNewBaseTemplateCommandIcon))
                {
                    _PullUpFieldsIntoNewBaseTemplateCommandIcon = Settings.GetSetting("Sitecore.PullUpFields.PullUpFieldsIntoNewBaseTemplateCommandIcon");
                }

                return _PullUpFieldsIntoNewBaseTemplateCommandIcon;
            }
        }

        private string _PullUpFieldsIntoExistingBaseTemplateCommandIcon;
        public string PullUpFieldsIntoExistingBaseTemplateCommandIcon
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PullUpFieldsIntoExistingBaseTemplateCommandIcon))
                {
                    _PullUpFieldsIntoExistingBaseTemplateCommandIcon = Settings.GetSetting("Sitecore.PullUpFields.PullUpFieldsIntoExistingBaseTemplateCommandIcon");
                }

                return _PullUpFieldsIntoExistingBaseTemplateCommandIcon;
            }
        }

        private string _PullUpFieldsIntoNewBaseTemplatePipelineName;
        public string PullUpFieldsIntoNewBaseTemplatePipelineName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PullUpFieldsIntoNewBaseTemplatePipelineName))
                {
                    _PullUpFieldsIntoNewBaseTemplatePipelineName = Settings.GetSetting("Sitecore.PullUpFields.PullUpFieldsIntoNewBaseTemplatePipelineName");
                }

                return _PullUpFieldsIntoNewBaseTemplatePipelineName;
            }
        }

        private string _PullUpFieldsIntoExistingBaseTemplatePipelineName;
        public string PullUpFieldsIntoExistingBaseTemplatePipelineName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PullUpFieldsIntoExistingBaseTemplatePipelineName))
                {
                    _PullUpFieldsIntoExistingBaseTemplatePipelineName = Settings.GetSetting("Sitecore.PullUpFields.PullUpFieldsIntoExistingBaseTemplatePipelineName");
                }

                return _PullUpFieldsIntoExistingBaseTemplatePipelineName;
            }
        }

        private PullUpFieldsSettings(ISettings settings)
        {
            SetSettings(settings);
        }

        private void SetSettings(ISettings settings)
        {
            Assert.ArgumentNotNull(settings, "settings");
            Settings = settings;
        }

        public static IPullUpFieldsSettings CreateNewPullUpFieldsSettings(ISettings settings)
        {
            return new PullUpFieldsSettings(settings);
        }
    }
}