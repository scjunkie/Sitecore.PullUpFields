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

using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.PullUpFields.Entities
{
    public class SectionInformation : IEquatable<SectionInformation>
    {
        public Item Section { get; set; }

        public SectionInformation(Item section)
        {
            Assert.ArgumentNotNull(section, "section");
            Section = section;
        }

        public override int GetHashCode()
        {
            return Section.ID.Guid.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return Equals(other as SectionInformation);
        }

        public bool Equals(SectionInformation other)
        {
            return other != null
                    && Section.ID.Guid == Section.ID.Guid;
        }
    }
}