using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace _Scripts
{
    public class UnitManager
    {
        private Dictionary<int, MVCSContext> _unitContexts;

        public UnitManager()
        {
            _unitContexts = new Dictionary<int, MVCSContext>();
        }

        public void RegisterUnit(int unitId, MVCSContext unitContext)
        {
            _unitContexts.Add(unitId, unitContext);
        }

        public MVCSContext GetUnitById(int unitId)
        {
            return _unitContexts[unitId];
        }
    }
}