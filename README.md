# TFS2NAV
WCF Service for connecting TFS to NAV

# Purpose

You can use this service as Service Hook in TFS (on prem or Teams Services) which will take the Json and send it into NAV Webservice as text.
Than you can do waht you want inside NAV with the data.

# Installation

Service needs to be hosted in IIS as WebApplication.

# Setup

In web.config specify the parameters in section applicationSettings, mainly WSUserName, WSUserPwd, WSUserDomain and URL for the NAV Web service. 

# NAV Web Service

The web service should have one parameter of type Text (no length limit) and should return Text. By default I am expecting NTLM. You canchange that in the 
web.config.
