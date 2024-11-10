using System;
using api.DTOs.Hotel;
using api.DTOs.Hotel.Requests;
using api.Models.Hotel;
using api.Services.Common;
using Microsoft.IdentityModel.Tokens;

namespace api.Services.Hotel
{
    public class HotelService(IUploadFile uploadFile)
    {
        public void DeleteGellaryItem(HotelModel current, List<Guid>? listGellary)
        {
            if (!listGellary.IsNullOrEmpty())
            {
                foreach (var item in listGellary!)
                {
                    var foundGallery = current.Gellary.FirstOrDefault(g => g.Id == item);
                    if (foundGallery != null)
                    {
                        current.Gellary.Remove(foundGallery);
                        uploadFile.Delete(foundGallery.Url);
                    }
                }
            }
        }

        public async Task<List<GalleryModel>> UploadImageHandling(
            List<CreateImageGalleryRequest>? Gellary
        )
        {
            List<GalleryModel> tempGellary = [];
            if (!Gellary.IsNullOrEmpty())
            {
                foreach (var item in Gellary!)
                {
                    tempGellary.Add(
                        new()
                        {
                            Url = await uploadFile.Create(item.File, @"Files\Images"),
                            Description = item.Description,
                        }
                    );
                }
            }
            return tempGellary;
        }
    }
}
