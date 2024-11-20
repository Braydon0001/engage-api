namespace Engage.Application.Models.Configuration
{
    public class ImageOptions
    {
        /// <summary>
        /// The path that contains survey photes. E.g. wwwroot/surveyphotos/ 
        /// </summary>
        public string SurveyPhotoFolderPath { get; set; }

        /// <summary>
        /// The folder that contains survey photos. E.g. surveyphotos 
        /// </summary>
        public string SurveyPhotoFolder { get; set; }
    }
}
