# Survey Photos

Survey photos are contained in a folder on the web server that hosts the Ensource web site. 

## Photo Folders

The base folder, for survey photos, is specified in the `Images` section of ``appsettings.json``.   
E.g: 
    
    "Images": {
        "SurveyPhotoFolder": "wwwroot/surveyphotos/"
    }

The base folder can either be a:  
  * Windows folder. E.g: ``C:\SurveyFolder\``, ""Or""    
  * Static folder in the web application. E.g: ``wwwroot/surveyphotos/``

The sub folders are organized as this hierarchy within the base folder:

     SupplierId
        EngageBrandId
            SurveyId 

The ``SurveyId`` ""folder"" contains all the photos for the particular survey.

## Photo Name

**TODO Morne. Has this changed?**   
The photo name is defined as ``[EmployeeStoreSurveyId]-[DateInSeconds].[FileExtension]``

E.g: ``283-1588414557550.jpg``

## Photo Url 

A photo can be fetched with a url which is defined as ``[BaseUrl]/[Folder]/[FileName].[FileExtension]``
  
E.g. ``https://ensource.insightconsulting.co.za/surveyphotos/18/4/10/23-1591040411.419-1591040417358.jpg``    


Where: 

| Url Part        | Description | 
| ------------- |:-------------:| 
| BaseUrl      | https://ensource.insightconsulting.co.za | 
| Folder      | surveyphotos/18/4/10  | 
| FileName | 23-1591040411.419-1591040417358      | 
| FileExtension | jpg | 
