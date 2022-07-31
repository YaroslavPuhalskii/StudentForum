namespace StudentForum.WebUI.Helpers.Image
{
    public static class ImageHelper
    {
        public static async Task<byte[]> ConvertPhotoToBytes(this IFormFile photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }

            await using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
