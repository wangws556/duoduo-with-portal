using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.Common.Web;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    [CookieAuthorize]
    public class ImageController : BaseController
    {
        public ActionResult Upload(ImageModel model)
        {
            return View("UploadImage", model);
        }

        private ActionResult SaveImage(string ownertype, int ownerid, int imageid, int imgTypeId, int oldImageId) 
        {            
            HttpPostedFileBase file = null;
            string result = YoYoStudio.Resource.Messages.UploadError;
            if (Request.Files.Count > 0)
            {
                result = YoYoStudio.Resource.Messages.UploadSucceed;
                file = Request.Files[0];
                if (file.InputStream.Length > 102400)
                {
                    result = string.Format(YoYoStudio.Resource.Messages.MaxFileUpload, "100");
                }
                else
                {
                    string name = Path.GetFileNameWithoutExtension(file.FileName);
                    string ext = Path.GetExtension(file.FileName);
                    byte[] data = new byte[file.InputStream.Length];
                    file.InputStream.Position = 0;
                    file.InputStream.CopyTo(new MemoryStream(data));
                    Image image = new Image { Id = imageid, OldId = oldImageId, Name = name, Ext = ext, ImageType_Id = imgTypeId, TheImage = data };
                    if (imgTypeId == BuiltIns.GiftImageType.Id)
                    {
                        var gift = GetEntity<Gift>(new int[] { ownerid });
                        if (gift != null)
                        {
                            if (gift.GiftGroup_Id.HasValue)
                            {
                                var giftGroup = GetEntity<GiftGroup>(new int[] { gift.GiftGroup_Id.Value });
                                if (giftGroup != null)
                                {
                                    image.ImageGroup = giftGroup.Name;
                                }
                            }
                        }
                    }
                    try
                    {
                        if (imageid > 0)
                        {
                            UpdateEntity(image);
                        }
                    }
                    catch (DatabaseException ex)
                    {
                        result = ex.Message;
                    }
                }
            }
            return View("UploadImage", new ImageModel(ownertype) { Id = imageid, OwnerId = ownerid, OldId=imageid, Message = result });
        }

        [HttpPost]
        public ActionResult SaveImage(string ownertype, int? ownerid, int? imageid, int? oldId)
        {
            if (!imageid.HasValue)
            {
                imageid = -1;
            }
            if (!oldId.HasValue)
            {
                oldId = imageid.Value;
            }
            if (ownerid.HasValue)
            {
                ImagedEntity entity = GetEntity(ownertype, new int[] { ownerid.Value }) as ImagedEntity;
				if (entity != null)
				{
					return SaveImage(ownertype, ownerid.Value, imageid.Value, entity.GetImageType().Id, oldId.Value);
				}
            }
            return View("UploadImage", new ImageModel(ownertype) { OwnerId = ownerid.Value, Image_Id = imageid.Value, OldId=imageid.Value});
        }
       
    }
}
