# FSEPMAPI
## Installation of Raven DB
  1) Download RavenDB fromhttps://ravendb.net/download. Alternatively you can use the docker image. if you plan to try cloud hosting then that is also acceptable, but this code has been tested with their DBASS offering.

  2) Get the license key from https://ravendb.net/license/request/community
  3) Run and brig up the cluster and update your license key. 
## Procedure to run the API codebase
    1) Download the code 
    2) Update the appSettings.JSON with URI of RavenDB 
      "RavenDb": {
        "Urls": [
          "<<RavenDB url here fromprevious step>>"
        ],
        "DatabaseName": "PMODB",
        "CertPath": "",
        "CertPass": null
      }
    3) Start the service using the exe or DLL based on platform like you start any executable
