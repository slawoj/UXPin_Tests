using static UXPinTests.Helpers.CommonHelpers;
using UXPinTests.Data;
using OpenQA.Selenium;
using System;
using NUnit.Framework;

namespace UXPinTests.PageObjects.Pages
{
    internal class PreviewPage : BasePage
    {
        public PreviewPage(IWebDriver driver) : base(driver)
        {
            CommentsData = new CommentsData(Driver);
        }
        public CommentsData CommentsData { get; }
        private By CommentContextButton => By.CssSelector("nav li[class*='tab']>a[class*='comment']");
        private By CommentContextActive => By.CssSelector("ul[class*='tabs-wrapper']>li[class*='active'] a[href][class*='icon-general-comment']");
        private By CommentContainer => By.XPath("//section[(contains(@class, 'comments-group selected') and (contains(@style,'display: block;')))]");
        private By CommentTextArea => By.XPath("//textarea");
        private By CommentAddButton => By.XPath("//button[contains(@class,'add-comment')]");
        private By PreviewContainer => By.ClassName("preview-container");
        private By CommentPin(int pinNumber) => By.XPath(String.Format("//section[contains(@class, 'comment-point') and not(contains(@class, 'adding-comment'))]/section[contains(@class, 'comment-pin') and text()='{0}']", pinNumber));
        private By CommentText(string commentText) => By.XPath(String.Format("//section[@class='comment-wrapper']//div[@class='comment-body']//span[text()='{0}']", commentText));
        private By CommentText(int commentNumber) => By.XPath(String.Format("(//section[@class='comment-wrapper']//div[@class='comment-body']//span[text()])[{0}]", commentNumber));
        private By CommentMenuButton(string commentText) => By.XPath(String.Format("//span[text()='{0}']/ancestor::div[@class='comment-body']/following-sibling::menu[contains(@class,'comment-menu')]", commentText));
        private By CommentEditButton(string commentText) => By.XPath(String.Format("//span[text()='{0}']/ancestor::div[@class='comment-body']/following-sibling::menu[contains(@class,'comment-menu')]//a[@class='edit']", commentText));
        bool IsCommentContextActiveAvailable => IsElementAvailable(Driver, CommentContextActive);
        bool IsCommentContainerAvailable => IsElementAvailable(Driver, CommentContainer);
        bool IsCommentTextAvailable(string commentText) => IsElementAvailable(Driver, CommentText(commentText));

        public void AddComment()
        {
            string commentText = "just a single comment";
            if (!(IsCommentContextActiveAvailable))
                WaitAndClickOnElement(Driver, CommentContextButton);
            if (!(IsCommentContainerAvailable))
                WaitAndClickOnElement(Driver, PreviewContainer);
            WaitAndClickOnElement(Driver, CommentTextArea);
            InputText(Driver, CommentTextArea, commentText);
            WaitAndClickOnElement(Driver, CommentAddButton);
            TakeScreenshot(Driver);
            Assert.IsTrue(IsCommentTextAvailable(commentText), "Comment text is not available. Should be {0}", commentText);
            LogMsg(String.Format("Comment \"{0}\" added", commentText));
        }

        public void AddComment(string commentText)
        {
            if (!(IsCommentContextActiveAvailable))
                WaitAndClickOnElement(Driver, CommentContextButton);
            if (!(IsCommentContainerAvailable))
                WaitAndClickOnElement(Driver, PreviewContainer);
            WaitAndClickOnElement(Driver, CommentTextArea);
            InputText(Driver, CommentTextArea, commentText);
            WaitAndClickOnElement(Driver, CommentAddButton);
            TakeScreenshot(Driver);
            Assert.IsTrue(IsCommentTextAvailable(commentText), "Comment text is not available. Should be {0}", commentText);
            LogMsg(String.Format("Comment \"{0}\" added", commentText));
        }

        public void EditComment(string newCommentText)
        {
            if (!(IsCommentContextActiveAvailable))
                WaitAndClickOnElement(Driver, CommentContextButton);
            if (CommentsData.PinsCount < 1)
            {
                Assert.Ignore("Comments are not available. Please add a comment first");
            }
            if (!(IsCommentContainerAvailable))
            {
                int pinNumber = CommentsData.RandomPinNumber;
                WaitAndClickOnElement(Driver, CommentPin(pinNumber));
            }
            int commentNumber = CommentsData.RandomCommentNumber;
            string commentText = GetElementText(Driver, CommentText(commentNumber));
            WaitAndClickOnElement(Driver, CommentMenuButton(commentText));
            WaitAndClickOnElement(Driver, CommentEditButton(commentText));
            InputText(Driver, CommentTextArea, newCommentText);
            WaitAndClickOnElement(Driver, CommentAddButton);
            TakeScreenshot(Driver);
            Assert.IsTrue(IsCommentTextAvailable(newCommentText), "Comment text is not available. Should be {0}", newCommentText);
            LogMsg(String.Format("Comment \"{0}\" edited. New comment is \"{1}\"", commentText, newCommentText));
        }
    }
}