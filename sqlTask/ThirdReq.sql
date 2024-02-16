SELECT TOP 1 [Department_ID]
FROM [Employee]
Group By [Department_ID]
Order By SUM([Salary]) DESC
