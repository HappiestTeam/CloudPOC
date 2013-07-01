Feature: CloudPOC-Push URLs


Scenario: Push single URL to cloud
	Given the CloudPOC website is accessible from web browser	
	When I push an  URL "URL" to cloud
	Then the URL shoud be listed in the Response result with status as "true"

Scenario: Push multiple URLs to cloud
	Given the CloudPOC website is accessible from web browser	
	When I push multiple URLS "URLs" to cloud
	Then the URLs shoud be listed in the Response result with status as "true"

Scenario: Search an URL pushed to cloud
	Given the CloudPOC website is accessible from web browser
	And the URL "URL" is already pushed in to cloud	
	When search this URL in cloud
	Then the search result should be indicate URL is present in cloud

	