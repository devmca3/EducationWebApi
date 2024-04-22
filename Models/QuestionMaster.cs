using System;
using System.Collections.Generic;

namespace EducationWebApi.Models;

public partial class QuestionMaster
{
    public int QuestionId { get; set; }

    public int? CourseId { get; set; }

    public int? SubjectId { get; set; }

    public string Question { get; set; } = null!;

    public string? Language { get; set; }

    public long? QuestionPhoto { get; set; }

    public string? OptionA { get; set; }

    public long? OptionAPhoto { get; set; }

    public string? OptionB { get; set; }

    public long? OptionBPhoto { get; set; }

    public string? OptionC { get; set; }

    public long? OptionCPhoto { get; set; }

    public string? OptionD { get; set; }

    public long? OptionDPhoto { get; set; }

    public string? OptionE { get; set; }

    public long? OptionEPhoto { get; set; }

    public string? CorrectAnswer { get; set; }

    public decimal? Marks { get; set; }

    public int QuestionTypeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? Status { get; set; }

    public bool? Deleted { get; set; }
}
