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

using Sitecore.PullUpFields.Configuration.Base;

namespace Sitecore.PullUpFields.Configuration
{
    public class SettingsFactory : ISettingsFactory
    {
        private SettingsFactory()
        {
        }

        public ISettings CreateNewSitecoreSettings()
        {
            return SitecoreSettings.CreateNewSitecoreSettings();
        }

        public IPullUpFieldsSettings CreateNewPullUpFieldsSettings()
        {
            return CreateNewPullUpFieldsSettings(CreateNewSitecoreSettings());
        }

        public IPullUpFieldsSettings CreateNewPullUpFieldsSettings(ISettings settings)
        {
            return PullUpFieldsSettings.CreateNewPullUpFieldsSettings(settings);
        }

        public static ISettingsFactory CreateNewSettingsFactory()
        {
            return new SettingsFactory();
        }
    }
}