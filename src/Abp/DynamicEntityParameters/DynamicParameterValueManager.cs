﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.DynamicEntityParameters
{
    public class DynamicParameterValueManager : IDynamicParameterValueManager, ITransientDependency
    {
        private readonly IDynamicParameterPermissionChecker _dynamicParameterPermissionChecker;

        public IDynamicParameterValueStore DynamicParameterValueStore { get; set; }

        public DynamicParameterValueManager(IDynamicParameterPermissionChecker dynamicParameterPermissionChecker)
        {
            _dynamicParameterPermissionChecker = dynamicParameterPermissionChecker;
            DynamicParameterValueStore = NullDynamicParameterValueStore.Instance;
        }

        public virtual DynamicParameterValue Get(int id)
        {
            var val = DynamicParameterValueStore.Get(id);
            _dynamicParameterPermissionChecker.CheckPermissions(val.DynamicParameterId);
            return val;
        }

        public virtual async Task<DynamicParameterValue> GetAsync(int id)
        {
            var val = await DynamicParameterValueStore.GetAsync(id);
            await _dynamicParameterPermissionChecker.CheckPermissionsAsync(val.DynamicParameterId);
            return val;
        }

        public virtual List<DynamicParameterValue> GetAll(int dynamicParameterId)
        {
            _dynamicParameterPermissionChecker.CheckPermissions(dynamicParameterId);
            return DynamicParameterValueStore.GetAll(dynamicParameterId);
        }

        public virtual async Task<List<DynamicParameterValue>> GetAllAsync(int dynamicParameterId)
        {
            await _dynamicParameterPermissionChecker.CheckPermissionsAsync(dynamicParameterId);
            return await DynamicParameterValueStore.GetAllAsync(dynamicParameterId);
        }

        public virtual void Add(DynamicParameterValue dynamicParameterValue)
        {
            _dynamicParameterPermissionChecker.CheckPermissions(dynamicParameterValue.DynamicParameterId);
            DynamicParameterValueStore.Add(dynamicParameterValue);
        }

        public virtual async Task AddAsync(DynamicParameterValue dynamicParameterValue)
        {
            await _dynamicParameterPermissionChecker.CheckPermissionsAsync(dynamicParameterValue.DynamicParameterId);
            await DynamicParameterValueStore.AddAsync(dynamicParameterValue);
        }

        public virtual void Update(DynamicParameterValue dynamicParameterValue)
        {
            _dynamicParameterPermissionChecker.CheckPermissions(dynamicParameterValue.DynamicParameterId);
            DynamicParameterValueStore.Update(dynamicParameterValue);
        }

        public virtual async Task UpdateAsync(DynamicParameterValue dynamicParameterValue)
        {
            await _dynamicParameterPermissionChecker.CheckPermissionsAsync(dynamicParameterValue.DynamicParameterId);
            await DynamicParameterValueStore.UpdateAsync(dynamicParameterValue);
        }

        public virtual void Delete(int id)
        {
            var val = Get(id);
            if (val != null)//Get checks permission, no need to check it again  
            {
                DynamicParameterValueStore.Delete(id);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            var val = await GetAsync(id);
            if (val != null)//Get checks permission, no need to check it again
            {
                await DynamicParameterValueStore.DeleteAsync(id);
            }
        }
    }
}