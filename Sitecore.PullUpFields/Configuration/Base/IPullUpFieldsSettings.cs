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

namespace Sitecore.PullUpFields.Configuration.Base
{
    public interface IPullUpFieldsSettings
    {
        char IDDelimiter { get; }

        string BaseTemplateFieldName { get; }

        string NotTemplateMessage { get; }

        string SelectFieldsDialogTitle { get; }

        string SelectFieldsDialogText { get; }

        string SelectFieldsDialogWidth { get; }

        string SelectFieldsDialogHeight { get; }

        string ChooseBaseTemplateNamePrompt { get; }

        string DefaultChooseBaseTemplateName { get; }

        string SelectBaseTemplateLocationDialogTitle { get; }

        string SelectBaseTemplateLocationDialogText { get; }

        string SelectBaseTemplateLocationDialogOKButtonText { get; }

        string SelectBaseTemplateLocationSaveRoot { get; }

        string ChooseExistingBaseTemplateDialogTitle { get; }

        string ChooseExistingBaseTemplateDialogText { get; }

        string ChooseExistingBaseTemplateDialogOKButtonText { get; }

        string CannotCreateItemAtSelectedLocationMessage { get; }

        string OperationCancelledMessage { get; }

        string PullUpFieldsIntoNewBaseTemplateCommandIcon { get; }

        string PullUpFieldsIntoExistingBaseTemplateCommandIcon { get; }

        string PullUpFieldsIntoNewBaseTemplatePipelineName { get; }

        string PullUpFieldsIntoExistingBaseTemplatePipelineName { get; }
    }
}
