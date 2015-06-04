﻿using ENTech.Store.Services.Misc;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreFindRequest : FindRequestBase<StoreLoadOption, StoreSortField, StoreFindCriteriaDto, BusinessAdminSecurityInformation>
	{
		public string Name { get; set; }
	}
}