using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudPOC.TestData;

namespace UITests.Tests 
{
    [TestClass]
    public partial class CloudPOC_PushURLs 
    {

        
        [TestMethod]
        public void PushSingleURLToCloud()
        {         
            Given_the_CloudPOC_website_is_accessible_from_web_browser();        
            When_I_push_an__URL__to_cloud(HomePageData.SingleUrl);        
            Then_the_URL_shoud_be_listed_in_the_Response_result_with_status_as("true");
        }
        
        [TestMethod]
        public void PushMultipleURLsToCloud()
        {         
            Given_the_CloudPOC_website_is_accessible_from_web_browser();        
            When_I_push_multiple_URLS__to_cloud(HomePageData.MultipleUrls);        
            Then_the_URLs_shoud_be_listed_in_the_Response_result_with_status_as("true");
        }
        
        [TestMethod]
        public void SearchAnURLPushedToCloud()
        {         
            Given_the_CloudPOC_website_is_accessible_from_web_browser();        
            And_the_URL__is_already_pushed_in_to_cloud(HomePageData.SingleUrl);        
            When_search_this_URL_in_cloud();        
            Then_the_search_result_should_be_indicate_URL_is_present_in_cloud();
        }

    }
}
