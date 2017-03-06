# TFS2NAV
WCF Service for connecting TFS to NAV

# Purpose

You can use this service as Service Hook in TFS (on prem or Team Services) which will take the Json and send it into NAV Webservice as text.
Than you can do what you want inside NAV with the data.

# Installation

Service needs to be hosted in IIS as WebApplication.

# Setup

In web.config specify the parameters in section application Settings, mainly WSUserName, WSUserPwd, WSUserDomain and URL for the NAV Web service. 

# TFS Setup

In TFS create new Service Hook of type Web Hook to address like http://server/TFS2NAV/TFS2NAVListener.svc/ToNAV and select which info you want to send to
this hook. The Json with info about the event will ends in your NAV.

# NAV Web Service

The web service should have one parameter of type Text (no length limit) and should return Text. By default I am expecting NTLM. You can change that in the 
web.config.
