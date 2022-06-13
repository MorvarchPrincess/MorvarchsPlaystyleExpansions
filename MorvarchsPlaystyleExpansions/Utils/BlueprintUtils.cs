using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorvarchsPlaystyleExpansions.Utils
{
    class BlueprintUtils
    {
        public static T GetBlueprint<T>(string id) where T : SimpleBlueprint
        {
            var assetId = new BlueprintGuid(System.Guid.Parse(id));
            return GetBlueprint<T>(assetId);
        }
        public static T GetBlueprint<T>(BlueprintGuid id) where T : SimpleBlueprint
        {
            SimpleBlueprint asset = ResourcesLibrary.TryGetBlueprint(id);
            if (asset == null) { Main.Log($"COULD NOT LOAD: {id} - {typeof(T)}"); }
            T value = asset as T;
            if (value == null) { Main.Log($"COULD NOT LOAD: {id} - {typeof(T)}"); }
            return value;
        }
    }
}
