﻿using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IBuildPCRepository
    {
        Task<IEnumerable<CategoryDTO>> listCategory();
        Task<IEnumerable<FilterDTO>> getFilterOfCategory(int cate_id);

    }
}
