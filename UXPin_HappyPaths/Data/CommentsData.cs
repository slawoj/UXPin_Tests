using static UXPinTests.Helpers.CommonHelpers;
using UXPinTests.PageObjects;
using OpenQA.Selenium;
using System;

namespace UXPinTests.Data
{
    internal class CommentsData : BasePage
    {
        public CommentsData(IWebDriver driver) : base(driver)
        {
        }
        public int RandomPinNumber => GetRandomNumber(PinsCount);
        public int RandomCommentNumber => GetRandomNumber(CommentsCount);
        public int PinsCount => CountElements(Driver, Pins);
        public int CommentsCount => CountElements(Driver, Comments);
        private By Pins => By.XPath(String.Format("//section[contains(@class,'comment-point') and not(contains(@class,'adding-comment'))]/section[contains(@class,'comment-pin')]"));
        private By Comments => By.XPath(String.Format("//section[@class='comment-wrapper']//div[@class='comment-body']//span"));
    }
}