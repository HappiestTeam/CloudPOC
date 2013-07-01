Feature: CloudPOC-Push URLs


Scenario: Push single URL to cloud
	Given the CloudPOC website is accessible from web browser	
	When I push an  URL "URL" to cloud
	Then the URL shoud be listed in the Response result with status as "status"

Scenario: Push multiple URLs to cloud
	Given the CloudPOC website is accessible from web browser	
	When I push multiple URLS "URLs" to cloud
	Then the URLs shoud be listed in the Response result with status as "status"

	