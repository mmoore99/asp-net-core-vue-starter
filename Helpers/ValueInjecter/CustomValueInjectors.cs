#region license

/*
Copyright 2010 Pedro Rui Silva

Licensed under Microsoft Public License (Ms-PL)  

Unless required by applicable law or agreed to in writing, software 
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/

#endregion

using System;
using System.Reflection;
using Fbits.VueMpaTemplate.Helpers.Extensions;
using Omu.ValueInjecter.Injections;

namespace Fbits.VueMpaTemplate.Helpers.ValueInjecter
{
    public class DateTimeOffsetToDateTime : LoopInjection
    {
        protected override bool MatchTypes(Type sourceType, Type targetType)
        {
            return sourceType == typeof(DateTimeOffset) && targetType == typeof(DateTime);
        }

        protected override void SetValue(object source, object target, PropertyInfo sourceProperty, PropertyInfo targetProperty)
        {
            var sourceValue = (DateTimeOffset) sourceProperty.GetValue(source);
            targetProperty.SetValue(target, sourceValue.ConvertToDateTime());
        }
    }

}