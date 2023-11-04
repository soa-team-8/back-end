﻿using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReportedIssueRepository
    {
        ReportedIssue Resolve(long id);
        ReportedIssue AddComment(long id, ReportedIssueComment comment);
        ReportedIssue AddDeadline(int id, DateTime deadline);
        PagedResult<ReportedIssue> GetPaged(int page, int pageSize);
        PagedResult<ReportedIssue> GetPagedByAuthor(long id, int page, int pageSize);
        PagedResult<ReportedIssue> GetPagedByTourist(long id, int page, int pageSize);
    }
}
