ALTER VIEW [dbo].[dt_ProductReply]
AS
SELECT  R.ReplyId ,
        R.ProductId ,
        R.MemberId ,
        R.Context ,
        R.IssueTime ,
        R.IsRead,
        CASE R.IsSysReply WHEN '1' THEN '' ELSE M.HeadPhoto END AS HeadPhoto ,
        CONVERT(VARCHAR(100), R.IssueTime, 20) FIssueTime ,
        R.IsSysReply
FROM    dbo.tbl_ProductReply R
        LEFT JOIN dbo.tbl_Member M ON M.MemberId = R.MemberId

		GO

