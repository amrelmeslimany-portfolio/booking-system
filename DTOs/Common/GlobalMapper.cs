using System;
using AutoMapper;

namespace api.DTOs.Common;

public class GlobalMapper : Profile
{
    public GlobalMapper()
    {
        CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
        CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
    }
}
