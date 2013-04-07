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

using System.Linq;

using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

using Sitecore.PullUpFields.Shell.Framework.Commands.Base;

namespace Sitecore.PullUpFields.Shell.Framework.Commands
{
    public class PullUpFieldsIntoExistingBaseTemplate : PullUpFieldsCommand
    {
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            bool shouldEnabled = base.QueryState(context) == CommandState.Enabled 
                                    && DoesTemplateHaveBaseTemplates(context);
            if (shouldEnabled)
            {
                return CommandState.Enabled;
            }

            return CommandState.Hidden;
        }

        private static bool DoesTemplateHaveBaseTemplates(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            TemplateItem templateItem = GetItem(context);
            return templateItem.BaseTemplates.Count() > 1 
                    || (templateItem.BaseTemplates.Any() 
                            && templateItem.BaseTemplates.FirstOrDefault().ID != TemplateIDs.StandardTemplate);
        }

        protected override string GetIcon()
        {
            return PullUpFieldsSettings.PullUpFieldsIntoExistingBaseTemplateCommandIcon;
        }

        protected override string GetPullUpFieldsClientPipelineName()
        {
            return PullUpFieldsSettings.PullUpFieldsIntoExistingBaseTemplatePipelineName;
        }
    }
}