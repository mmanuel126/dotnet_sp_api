using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnet_sp_api.Models.DBContextModels;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tbad> Tbads { get; set; }

    public virtual DbSet<Tbcollege> Tbcolleges { get; set; }

    public virtual DbSet<Tbcontact> Tbcontacts { get; set; }

    public virtual DbSet<Tbcontactrequest> Tbcontactrequests { get; set; }

    public virtual DbSet<Tbday> Tbdays { get; set; }

    public virtual DbSet<Tbdegreetype> Tbdegreetypes { get; set; }

    public virtual DbSet<Tbforgotpwdcode> Tbforgotpwdcodes { get; set; }

    public virtual DbSet<Tbinterest> Tbinterests { get; set; }

    public virtual DbSet<Tbmajor> Tbmajors { get; set; }

    public virtual DbSet<Tbmember> Tbmembers { get; set; }

    public virtual DbSet<Tbmemberfollowing> Tbmemberfollowings { get; set; }

    public virtual DbSet<Tbmembernotification> Tbmembernotifications { get; set; }

    public virtual DbSet<Tbmemberpost> Tbmemberposts { get; set; }

    public virtual DbSet<Tbmemberpostresponse> Tbmemberpostresponses { get; set; }

    public virtual DbSet<Tbmemberprofile> Tbmemberprofiles { get; set; }

    public virtual DbSet<Tbmemberprofilecontactinfo> Tbmemberprofilecontactinfos { get; set; }

    public virtual DbSet<Tbmemberprofileeducationv2> Tbmemberprofileeducationv2s { get; set; }

    public virtual DbSet<Tbmemberprofilepersonalinfo> Tbmemberprofilepersonalinfos { get; set; }

    public virtual DbSet<Tbmemberprofilepicture> Tbmemberprofilepictures { get; set; }

    public virtual DbSet<Tbmembersprivacysetting> Tbmembersprivacysettings { get; set; }

    public virtual DbSet<Tbmembersregistered> Tbmembersregistereds { get; set; }

    public virtual DbSet<Tbmessage> Tbmessages { get; set; }

    public virtual DbSet<Tbmonth> Tbmonths { get; set; }

    public virtual DbSet<Tbnotification> Tbnotifications { get; set; }

    public virtual DbSet<Tbnotificationsetting> Tbnotificationsettings { get; set; }

    public virtual DbSet<Tbprivateschool> Tbprivateschools { get; set; }

    public virtual DbSet<Tbpublicschool> Tbpublicschools { get; set; }

    public virtual DbSet<Tbrecentnews> Tbrecentnews { get; set; }

    public virtual DbSet<Tbschooltype> Tbschooltypes { get; set; }

    public virtual DbSet<Tbsport> Tbsports { get; set; }

    public virtual DbSet<Tbstate> Tbstates { get; set; }

    public virtual DbSet<Tbyear> Tbyears { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tbad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbads_pkey");

            entity.ToTable("tbads");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Headertext)
                .HasMaxLength(150)
                .HasColumnName("headertext");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(100)
                .HasColumnName("imageurl");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Navigateurl)
                .HasMaxLength(200)
                .HasColumnName("navigateurl");
            entity.Property(e => e.Postingdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("postingdate");
            entity.Property(e => e.Textfield)
                .HasMaxLength(2000)
                .HasColumnName("textfield");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Tbcollege>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("tbcolleges_pkey");

            entity.ToTable("tbcolleges");

            entity.Property(e => e.SchoolId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("school_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.AwardsOffered)
                .HasMaxLength(500)
                .HasColumnName("awards_offered");
            entity.Property(e => e.CampusHousing)
                .HasMaxLength(150)
                .HasColumnName("campus_housing");
            entity.Property(e => e.CampusSetting)
                .HasMaxLength(90)
                .HasColumnName("campus_setting");
            entity.Property(e => e.Imagefile)
                .HasMaxLength(50)
                .HasColumnName("imagefile");
            entity.Property(e => e.Ipedsid)
                .HasMaxLength(150)
                .HasColumnName("ipedsid");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Opeid)
                .HasMaxLength(50)
                .HasColumnName("opeid");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .HasColumnName("state");
            entity.Property(e => e.StudentPopulation).HasColumnName("student_population");
            entity.Property(e => e.StudentToFacultyRatio)
                .HasMaxLength(50)
                .HasColumnName("Student_to_faculty_ratio");
            entity.Property(e => e.Type)
                .HasMaxLength(250)
                .HasColumnName("type");
            entity.Property(e => e.UndergradStudents).HasColumnName("undergrad_students");
            entity.Property(e => e.Website)
                .HasMaxLength(150)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Tbcontact>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.ContactId }).HasName("tbfriends_pkey");

            entity.ToTable("tbcontacts");

            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Datestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datestamp");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Contact).WithMany(p => p.TbcontactContacts)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbcontacts_tbmembers1");

            entity.HasOne(d => d.Member).WithMany(p => p.TbcontactMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbcontacts_tbmembers");
        });

        modelBuilder.Entity<Tbcontactrequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("tbcontactrequests_pkey");

            entity.ToTable("tbcontactrequests");

            entity.Property(e => e.RequestId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("request_id");
            entity.Property(e => e.FromMemberId).HasColumnName("from_member_id");
            entity.Property(e => e.RequestDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("request_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ToMemberId).HasColumnName("to_member_id");

            entity.HasOne(d => d.FromMember).WithMany(p => p.TbcontactrequestFromMembers)
                .HasForeignKey(d => d.FromMemberId)
                .HasConstraintName("fk_tbcontactrequests_tbmembers");

            entity.HasOne(d => d.ToMember).WithMany(p => p.TbcontactrequestToMembers)
                .HasForeignKey(d => d.ToMemberId)
                .HasConstraintName("fk_tbcontactrequests_tbmembers1");
        });

        modelBuilder.Entity<Tbday>(entity =>
        {
            entity.HasKey(e => e.Day).HasName("tbdays_pkey");

            entity.ToTable("tbdays");

            entity.HasIndex(e => e.Day, "tbdays_Day_key").IsUnique();

            entity.HasIndex(e => e.Day, "tbdays_day_key").IsUnique();

            entity.Property(e => e.Day)
                .HasMaxLength(2)
                .HasColumnName("day");
        });

        modelBuilder.Entity<Tbdegreetype>(entity =>
        {
            entity.HasKey(e => e.DegreeTypeId).HasName("tbdegreetype_pkey");

            entity.ToTable("tbdegreetype");

            entity.HasIndex(e => e.DegreeTypeId, "tbdegreetype_DegreeTypeID_key").IsUnique();

            entity.HasIndex(e => e.DegreeTypeId, "tbdegreetype_degree_type_id_key").IsUnique();

            entity.Property(e => e.DegreeTypeId)
                .ValueGeneratedNever()
                .HasColumnName("degree_type_id");
            entity.Property(e => e.DegreeTypeDesc)
                .HasMaxLength(50)
                .HasColumnName("degree_type_desc");
        });

        modelBuilder.Entity<Tbforgotpwdcode>(entity =>
        {
            entity.HasKey(e => e.CodeId).HasName("tbforgotpwdcodes_pkey");

            entity.ToTable("tbforgotpwdcodes");

            entity.HasIndex(e => e.CodeId, "tbforgotpwdcodes_CodeID_key").IsUnique();

            entity.HasIndex(e => e.CodeId, "tbforgotpwdcodes_code_id_key").IsUnique();

            entity.Property(e => e.CodeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("code_id");
            entity.Property(e => e.Codedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("codedate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Tbinterest>(entity =>
        {
            entity.HasKey(e => e.InterestId).HasName("tbinterests_pkey");

            entity.ToTable("tbinterests");

            entity.HasIndex(e => e.InterestId, "tbinterests_InterestID_key").IsUnique();

            entity.HasIndex(e => e.InterestId, "tbinterests_interest_id_key").IsUnique();

            entity.Property(e => e.InterestId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("interest_id");
            entity.Property(e => e.InterestDesc)
                .HasMaxLength(150)
                .HasColumnName("interest_desc");
            entity.Property(e => e.InterestType)
                .HasMaxLength(20)
                .HasColumnName("interest_type");
        });

        modelBuilder.Entity<Tbmajor>(entity =>
        {
            entity.HasKey(e => e.MajorId).HasName("tbmajors_pkey");

            entity.ToTable("tbmajors");

            entity.HasIndex(e => e.MajorId, "tbmajors_MajorID_key").IsUnique();

            entity.HasIndex(e => e.MajorId, "tbmajors_major_id_key").IsUnique();

            entity.Property(e => e.MajorId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("major_id");
            entity.Property(e => e.MajorDesc)
                .HasMaxLength(100)
                .HasColumnName("major_desc");
        });

        modelBuilder.Entity<Tbmember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("tbmembers_pkey");

            entity.ToTable("tbmembers");

            entity.HasIndex(e => e.MemberId, "tbmembers_MemberID_key").IsUnique();

            entity.HasIndex(e => e.MemberId, "tbmembers_member_id_key").IsUnique();

            entity.Property(e => e.MemberId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("member_id");
            entity.Property(e => e.ChatStatus).HasColumnName("chat_status");
            entity.Property(e => e.DeactivateExplanation)
                .HasMaxLength(1000)
                .HasColumnName("deactivate_explanation");
            entity.Property(e => e.DeactivateReason).HasColumnName("deactivate_reason");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FutureEmails)
                .HasPrecision(1)
                .HasColumnName("future_emails");
            entity.Property(e => e.LogOn)
                .HasPrecision(1)
                .HasColumnName("log_on");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.SecurityAnswer)
                .HasMaxLength(50)
                .HasColumnName("security_answer");
            entity.Property(e => e.SecurityQuestion).HasColumnName("security_question");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.YoutubeChannel)
                .HasMaxLength(100)
                .HasColumnName("youtube_channel");
        });

        modelBuilder.Entity<Tbmemberfollowing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbmemberfollowing_pkey");

            entity.ToTable("tbmemberfollowing");

            entity.HasIndex(e => e.Id, "tbmemberfollowing_Id_key").IsUnique();

            entity.HasIndex(e => e.Id, "tbmemberfollowing_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.FollowingMemberId).HasColumnName("following_member_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
        });

        modelBuilder.Entity<Tbmembernotification>(entity =>
        {
            entity.HasKey(e => new { e.NotificationMemberId, e.MemberId, e.NotificationId }).HasName("tbmembernotifications_pkey");

            entity.ToTable("tbmembernotifications");

            entity.Property(e => e.NotificationMemberId)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("notification_member_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
        });

        modelBuilder.Entity<Tbmemberpost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("tbmemberposts_pkey");

            entity.ToTable("tbmemberposts");

            entity.HasIndex(e => e.PostId, "tbmemberposts_PostID_key").IsUnique();

            entity.HasIndex(e => e.PostId, "tbmemberposts_post_id_key").IsUnique();

            entity.Property(e => e.PostId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("post_id");
            entity.Property(e => e.AttachFile)
                .HasMaxLength(100)
                .HasColumnName("attach_file");
            entity.Property(e => e.Description)
                .HasMaxLength(700)
                .HasColumnName("description");
            entity.Property(e => e.FileType).HasColumnName("file_type");
            entity.Property(e => e.LikeCounter).HasColumnName("like_counter");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.PostDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("post_date");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Member).WithMany(p => p.Tbmemberposts)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_tbmemberposts_tbmembers");
        });

        modelBuilder.Entity<Tbmemberpostresponse>(entity =>
        {
            entity.HasKey(e => e.PostResponseId).HasName("tbmemberpostresponses_pkey");

            entity.ToTable("tbmemberpostresponses");

            entity.HasIndex(e => e.PostResponseId, "tbmemberpostresponses_PostResponseID_key").IsUnique();

            entity.HasIndex(e => e.PostResponseId, "tbmemberpostresponses_post_response_id_key").IsUnique();

            entity.Property(e => e.PostResponseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("post_response_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ResponseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("response_date");

            entity.HasOne(d => d.Member).WithMany(p => p.Tbmemberpostresponses)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("fk_tbmemberpostresponses_tbmembers");

            entity.HasOne(d => d.Post).WithMany(p => p.Tbmemberpostresponses)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("fk_tbmemberpostresponses_tbmemberposts");
        });

        modelBuilder.Entity<Tbmemberprofile>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("tbmemberprofile_pkey");

            entity.ToTable("tbmemberprofile");

            entity.HasIndex(e => e.MemberId, "tbmemberprofile_MemberID_key").IsUnique();

            entity.HasIndex(e => e.MemberId, "tbmemberprofile_member_id_key").IsUnique();

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("member_id");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.CurrentCity)
                .HasMaxLength(50)
                .HasColumnName("current_city");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .HasColumnName("current_status");
            entity.Property(e => e.DobDay)
                .HasMaxLength(3)
                .HasColumnName("dob_day");
            entity.Property(e => e.DobMonth)
                .HasMaxLength(3)
                .HasColumnName("dob_month");
            entity.Property(e => e.DobYear)
                .HasMaxLength(5)
                .HasColumnName("dob_year");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Height)
                .HasMaxLength(20)
                .HasColumnName("height");
            entity.Property(e => e.HomeNeighborhood)
                .HasMaxLength(50)
                .HasColumnName("home_neighborhood");
            entity.Property(e => e.Hometown)
                .HasMaxLength(50)
                .HasColumnName("hometown");
            entity.Property(e => e.InterestedInType).HasColumnName("interested_in_type");
            entity.Property(e => e.JoinedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.LeftRightHandFoot)
                .HasMaxLength(30)
                .HasColumnName("left_right_hand_foot");
            entity.Property(e => e.LookingForEmployment).HasColumnName("looking_for_employment");
            entity.Property(e => e.LookingForNetworking).HasColumnName("looking_for_networking");
            entity.Property(e => e.LookingForPartnership).HasColumnName("looking_for_partnership");
            entity.Property(e => e.LookingForRecruitment).HasColumnName("looking_for_recruitment");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.PicturePath)
                .HasMaxLength(150)
                .HasColumnName("picture_path");
            entity.Property(e => e.PreferredPosition)
                .HasMaxLength(50)
                .HasColumnName("preferred_position");
            entity.Property(e => e.SecondaryPosition)
                .HasMaxLength(50)
                .HasColumnName("secondary_position");
            entity.Property(e => e.Sex)
                .HasMaxLength(20)
                .HasColumnName("sex");
            entity.Property(e => e.ShowDobType).HasColumnName("show_dob_type");
            entity.Property(e => e.ShowSexInProfile).HasColumnName("show_sex_in_profile");
            entity.Property(e => e.Sport)
                .HasMaxLength(50)
                .HasColumnName("sport");
            entity.Property(e => e.TitleDesc)
                .HasMaxLength(200)
                .HasColumnName("title_desc");
            entity.Property(e => e.Weight)
                .HasMaxLength(20)
                .HasColumnName("weight");

            entity.HasOne(d => d.Member).WithOne(p => p.Tbmemberprofile)
                .HasForeignKey<Tbmemberprofile>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbmemberprofile_tbmembers");
        });

        modelBuilder.Entity<Tbmemberprofilecontactinfo>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("tbmemberprofilecontactinfo_pkey");

            entity.ToTable("tbmemberprofilecontactinfo");

            entity.HasIndex(e => e.MemberId, "tbmemberprofilecontactinfo_MemberID_key").IsUnique();

            entity.HasIndex(e => e.MemberId, "tbmemberprofilecontactinfo_member_id_key").IsUnique();

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("member_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CellPhone)
                .HasMaxLength(20)
                .HasColumnName("cell_phone");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Facebook)
                .HasMaxLength(100)
                .HasColumnName("facebook");
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .HasColumnName("home_phone");
            entity.Property(e => e.Instagram)
                .HasMaxLength(100)
                .HasColumnName("instagram");
            entity.Property(e => e.Neighborhood)
                .HasMaxLength(50)
                .HasColumnName("neighborhood");
            entity.Property(e => e.OtherEmail)
                .HasMaxLength(100)
                .HasColumnName("other_email");
            entity.Property(e => e.OtherPhone)
                .HasMaxLength(20)
                .HasColumnName("other_phone");
            entity.Property(e => e.ShowAddress)
                .HasPrecision(1)
                .HasColumnName("show_address");
            entity.Property(e => e.ShowCellPhone)
                .HasPrecision(1)
                .HasColumnName("show_cell_phone");
            entity.Property(e => e.ShowEmailToMembers)
                .HasPrecision(1)
                .HasColumnName("show_email_to_members");
            entity.Property(e => e.ShowHomePhone)
                .HasPrecision(1)
                .HasColumnName("show_home_phone");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Twitter)
                .HasMaxLength(300)
                .HasColumnName("twitter");
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .HasColumnName("website");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .HasColumnName("zip");

            entity.HasOne(d => d.Member).WithOne(p => p.Tbmemberprofilecontactinfo)
                .HasForeignKey<Tbmemberprofilecontactinfo>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbmemberprofilecontactinfo_tbmembers");
        });

        modelBuilder.Entity<Tbmemberprofileeducationv2>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.SchoolId, e.SchoolType }).HasName("tbmemberprofileeducationv2_pkey");

            entity.ToTable("tbmemberprofileeducationv2");

            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.SchoolId).HasColumnName("school_id");
            entity.Property(e => e.SchoolType).HasColumnName("school_type");
            entity.Property(e => e.ClassYear)
                .HasMaxLength(10)
                .HasColumnName("class_year");
            entity.Property(e => e.DegreeType).HasColumnName("degree_type");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Major)
                .HasMaxLength(100)
                .HasColumnName("major");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(100)
                .HasColumnName("school_name");
            entity.Property(e => e.Societies)
                .HasMaxLength(300)
                .HasColumnName("societies");
            entity.Property(e => e.SportLevelType)
                .HasMaxLength(20)
                .HasColumnName("sport_level_type");
        });

        modelBuilder.Entity<Tbmemberprofilepersonalinfo>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("tbmemberprofilepersonalinfo_pkey");

            entity.ToTable("tbmemberprofilepersonalinfo");

            entity.HasIndex(e => e.MemberId, "tbmemberprofilepersonalinfo_MemberID_key").IsUnique();

            entity.HasIndex(e => e.MemberId, "tbmemberprofilepersonalinfo_member_id_key").IsUnique();

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("member_id");
            entity.Property(e => e.AboutMe).HasColumnName("about_me");
            entity.Property(e => e.Activities).HasColumnName("activities");
            entity.Property(e => e.FavoriteBooks).HasColumnName("favorite_books");
            entity.Property(e => e.FavoriteMovies).HasColumnName("favorite_movies");
            entity.Property(e => e.FavoriteMusic).HasColumnName("favorite_music");
            entity.Property(e => e.FavoriteQuotations).HasColumnName("favorite_quotations");
            entity.Property(e => e.FavoriteTvShows).HasColumnName("favorite_tv_shows");
            entity.Property(e => e.Interests).HasColumnName("interests");
            entity.Property(e => e.SpecialSkills).HasColumnName("special_skills");

            entity.HasOne(d => d.Member).WithOne(p => p.Tbmemberprofilepersonalinfo)
                .HasForeignKey<Tbmemberprofilepersonalinfo>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbmemberprofilepersonalinfo_tbmembers");
        });

        modelBuilder.Entity<Tbmemberprofilepicture>(entity =>
        {
            entity.HasKey(e => new { e.ProfileId, e.MemberId }).HasName("tbmemberprofilepictures_pkey");

            entity.ToTable("tbmemberprofilepictures");

            entity.Property(e => e.ProfileId)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("profile_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .HasColumnName("file_name");
            entity.Property(e => e.IsDefault)
                .HasPrecision(1)
                .HasColumnName("is_default");
            entity.Property(e => e.Removed)
                .HasPrecision(1)
                .HasColumnName("removed");
        });

        modelBuilder.Entity<Tbmembersprivacysetting>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.MemberId }).HasName("tbmembersprivacysettings_pkey");

            entity.ToTable("tbmembersprivacysettings");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.BasicInfo).HasColumnName("basic_info");
            entity.Property(e => e.ContactInfo).HasColumnName("contact_info");
            entity.Property(e => e.Education).HasColumnName("education");
            entity.Property(e => e.EmailAddress).HasColumnName("email_address");
            entity.Property(e => e.ImDisplayName).HasColumnName("im_display_name");
            entity.Property(e => e.MobilePhone).HasColumnName("mobile_phone");
            entity.Property(e => e.OtherPhone).HasColumnName("other_phone");
            entity.Property(e => e.PersonalInfo).HasColumnName("personal_info");
            entity.Property(e => e.PhotosTagOfYou).HasColumnName("photos_tag_of_you");
            entity.Property(e => e.Profile).HasColumnName("profile");
            entity.Property(e => e.VideosTagOfYou).HasColumnName("videos_tag_of_you");
            entity.Property(e => e.ViewFriendsList)
                .HasPrecision(1)
                .HasColumnName("view_friends_list");
            entity.Property(e => e.ViewLinkToRequestAddingYouAsFriend)
                .HasPrecision(1)
                .HasColumnName("view_link_to_request_adding_you_as_friend");
            entity.Property(e => e.ViewLinkToSendYouMsg)
                .HasPrecision(1)
                .HasColumnName("view_link_to_send_you_msg");
            entity.Property(e => e.ViewProfilePicture)
                .HasPrecision(1)
                .HasColumnName("view_profile_picture");
            entity.Property(e => e.Visibility).HasColumnName("visibility");
            entity.Property(e => e.WorkInfo).HasColumnName("work_info");

            entity.HasOne(d => d.Member).WithMany(p => p.Tbmembersprivacysettings)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbmemberprivacysettings_tbmembers");
        });

        modelBuilder.Entity<Tbmembersregistered>(entity =>
        {
            entity.HasKey(e => new { e.MemberCodeId, e.MemberId }).HasName("tbmembersregistered_pkey");

            entity.ToTable("tbmembersregistered");

            entity.HasIndex(e => e.MemberCodeId, "tbmembersregistered_MemberCodeID_key").IsUnique();

            entity.HasIndex(e => e.MemberCodeId, "tbmembersregistered_member_code_id_key").IsUnique();

            entity.Property(e => e.MemberCodeId)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("member_code_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.RegisteredDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registered_date");

            entity.HasOne(d => d.Member).WithMany(p => p.Tbmembersregistereds)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbmembersregistered_tbmembers");
        });

        modelBuilder.Entity<Tbmessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("tbmessages_pkey");

            entity.ToTable("tbmessages");

            entity.HasIndex(e => e.MessageId, "tbmessages_MessageID_key").IsUnique();

            entity.HasIndex(e => e.MessageId, "tbmessages_message_id_key").IsUnique();

            entity.Property(e => e.MessageId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("message_id");
            entity.Property(e => e.Attachment)
                .HasPrecision(1)
                .HasColumnName("attachment");
            entity.Property(e => e.AttachmentFile)
                .HasMaxLength(150)
                .HasColumnName("attachment_file");
            entity.Property(e => e.Body)
                .HasMaxLength(500)
                .HasColumnName("body");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.FlagLevel).HasColumnName("flag_level");
            entity.Property(e => e.ImportanceLevel).HasColumnName("importance_level");
            entity.Property(e => e.MessageState).HasColumnName("message_state");
            entity.Property(e => e.MsgDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("msg_date");
            entity.Property(e => e.OriginalMsg)
                .HasMaxLength(100)
                .HasColumnName("original_msg");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasColumnName("source");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");

            entity.HasOne(d => d.Contact).WithMany(p => p.TbmessageContacts)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("fk_tbmessages_tbmembers1");

            entity.HasOne(d => d.Sender).WithMany(p => p.TbmessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("fk_tbmessages_tbmembers");
        });

        modelBuilder.Entity<Tbmonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbmonths");

            entity.Property(e => e.Desc)
                .HasMaxLength(20)
                .HasColumnName("desc");
            entity.Property(e => e.Month)
                .HasMaxLength(2)
                .HasColumnName("month");
        });

        modelBuilder.Entity<Tbnotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("tbnotifications_pkey");

            entity.ToTable("tbnotifications");

            entity.HasIndex(e => e.NotificationId, "tbnotifications_NotificationID_key").IsUnique();

            entity.HasIndex(e => e.NotificationId, "tbnotifications_notification_id_key").IsUnique();

            entity.Property(e => e.NotificationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("notification_id");
            entity.Property(e => e.Notification)
                .HasMaxLength(2000)
                .HasColumnName("notification");
            entity.Property(e => e.SentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sent_date");
            entity.Property(e => e.Status)
                .HasPrecision(1)
                .HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");
        });

        modelBuilder.Entity<Tbnotificationsetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbnotificationsettings_pkey");

            entity.ToTable("tbnotificationsettings");

            entity.HasIndex(e => e.Id, "tbnotificationsettings_ID_key").IsUnique();

            entity.HasIndex(e => e.Id, "tbnotificationsettings_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddAsFriend)
                .HasPrecision(1)
                .HasColumnName("add_as_friend");
            entity.Property(e => e.AddsFriendYouSuggest)
                .HasPrecision(1)
                .HasColumnName("adds_friend_you_suggest");
            entity.Property(e => e.CommentAfterYouInVideo)
                .HasPrecision(1)
                .HasColumnName("comment_after_you_in_video");
            entity.Property(e => e.CommentOnVideo)
                .HasPrecision(1)
                .HasColumnName("comment_on_video");
            entity.Property(e => e.CommentOnVideoOfYou)
                .HasPrecision(1)
                .HasColumnName("comment_on_video_of_you");
            entity.Property(e => e.ConfirmFriendDetails)
                .HasPrecision(1)
                .HasColumnName("confirm_friend_details");
            entity.Property(e => e.ConfirmFriendshipRequest)
                .HasPrecision(1)
                .HasColumnName("confirm_friendship_request");
            entity.Property(e => e.HasBirthdayComingup)
                .HasPrecision(1)
                .HasColumnName("has_birthday_comingup");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Poking)
                .HasPrecision(1)
                .HasColumnName("poking");
            entity.Property(e => e.RepliesToYourHelpQuest)
                .HasPrecision(1)
                .HasColumnName("replies_to_your_help_quest");
            entity.Property(e => e.RequestToListAsFamily)
                .HasPrecision(1)
                .HasColumnName("request_to_list_as_family");
            entity.Property(e => e.SendMsg)
                .HasPrecision(1)
                .HasColumnName("send_msg");
            entity.Property(e => e.TagInPhoto)
                .HasPrecision(1)
                .HasColumnName("tag_In_Photo");
            entity.Property(e => e.TagOneOfYourPhotos)
                .HasPrecision(1)
                .HasColumnName("tag_one_of_your_photos");
            entity.Property(e => e.TagsInVideo)
                .HasPrecision(1)
                .HasColumnName("tags_in_video");
            entity.Property(e => e.TagsOneOfYourVideos)
                .HasPrecision(1)
                .HasColumnName("tags_one_of_your_videos");

            entity.HasOne(d => d.Member).WithMany(p => p.Tbnotificationsettings)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tbnotificationsettings_tbmembers");
        });

        modelBuilder.Entity<Tbprivateschool>(entity =>
        {
            entity.HasKey(e => e.LgId).HasName("tbprivateschools_pkey");

            entity.ToTable("tbprivateschools");

            entity.HasIndex(e => e.LgId, "tbprivateschools_LGId_key").IsUnique();

            entity.HasIndex(e => e.LgId, "tbprivateschools_lg_id_key").IsUnique();

            entity.Property(e => e.LgId)
                .ValueGeneratedNever()
                .HasColumnName("lg_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.County)
                .HasMaxLength(255)
                .HasColumnName("county");
            entity.Property(e => e.HiGrade)
                .HasMaxLength(255)
                .HasColumnName("hi_grade");
            entity.Property(e => e.ImageFile)
                .HasMaxLength(255)
                .HasColumnName("image_file");
            entity.Property(e => e.LoGrade)
                .HasMaxLength(255)
                .HasColumnName("Lo_grade");
            entity.Property(e => e.PPacislPct)
                .HasMaxLength(255)
                .HasColumnName("P_PACISL_PCT");
            entity.Property(e => e.PTwomorePct)
                .HasMaxLength(255)
                .HasColumnName("P_TWOMORE_PCT");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.PssAsianPct)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASIAN_PCT");
            entity.Property(e => e.PssAssoc1)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_1");
            entity.Property(e => e.PssAssoc2)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_2");
            entity.Property(e => e.PssAssoc3)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_3");
            entity.Property(e => e.PssAssoc4)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_4");
            entity.Property(e => e.PssAssoc5)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_5");
            entity.Property(e => e.PssAssoc6)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_6");
            entity.Property(e => e.PssAssoc7)
                .HasMaxLength(255)
                .HasColumnName("PSS_ASSOC_7");
            entity.Property(e => e.PssBlackPct)
                .HasMaxLength(255)
                .HasColumnName("PSS_BLACK_PCT");
            entity.Property(e => e.PssCoed)
                .HasMaxLength(255)
                .HasColumnName("PSS_COED");
            entity.Property(e => e.PssCommType)
                .HasMaxLength(255)
                .HasColumnName("PSS_COMM_TYPE");
            entity.Property(e => e.PssCountyFips)
                .HasMaxLength(255)
                .HasColumnName("pss_county_fips");
            entity.Property(e => e.PssCountyNo)
                .HasMaxLength(255)
                .HasColumnName("pss_county_no");
            entity.Property(e => e.PssEnroll1)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_1");
            entity.Property(e => e.PssEnroll10)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_10");
            entity.Property(e => e.PssEnroll11)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_11");
            entity.Property(e => e.PssEnroll12)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_12");
            entity.Property(e => e.PssEnroll2)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_2");
            entity.Property(e => e.PssEnroll3)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_3");
            entity.Property(e => e.PssEnroll4)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_4");
            entity.Property(e => e.PssEnroll5)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_5");
            entity.Property(e => e.PssEnroll6)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_6");
            entity.Property(e => e.PssEnroll7)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_7");
            entity.Property(e => e.PssEnroll8)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_8");
            entity.Property(e => e.PssEnroll9)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_9");
            entity.Property(e => e.PssEnrollK)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_K");
            entity.Property(e => e.PssEnrollPk)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_PK");
            entity.Property(e => e.PssEnrollT)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_T");
            entity.Property(e => e.PssEnrollTk12)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_TK12");
            entity.Property(e => e.PssEnrollUg)
                .HasMaxLength(255)
                .HasColumnName("PSS_ENROLL_UG");
            entity.Property(e => e.PssFips)
                .HasMaxLength(255)
                .HasColumnName("pss_fips");
            entity.Property(e => e.PssFteTeach)
                .HasMaxLength(255)
                .HasColumnName("PSS_FTE_TEACH");
            entity.Property(e => e.PssHispPct)
                .HasMaxLength(255)
                .HasColumnName("PSS_HISP_PCT");
            entity.Property(e => e.PssIndianPct)
                .HasMaxLength(255)
                .HasColumnName("PSS_INDIAN_PCT");
            entity.Property(e => e.PssLevel)
                .HasMaxLength(255)
                .HasColumnName("PSS_LEVEL");
            entity.Property(e => e.PssLibrary)
                .HasMaxLength(255)
                .HasColumnName("PSS_LIBRARY");
            entity.Property(e => e.PssLocale)
                .HasMaxLength(255)
                .HasColumnName("PSS_LOCALE");
            entity.Property(e => e.PssOrient)
                .HasMaxLength(255)
                .HasColumnName("PSS_ORIENT");
            entity.Property(e => e.PssRace2)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_2");
            entity.Property(e => e.PssRaceAi)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_AI");
            entity.Property(e => e.PssRaceAs)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_AS");
            entity.Property(e => e.PssRaceB)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_B");
            entity.Property(e => e.PssRaceH)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_H");
            entity.Property(e => e.PssRaceP)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_P");
            entity.Property(e => e.PssRaceW)
                .HasMaxLength(255)
                .HasColumnName("PSS_RACE_W");
            entity.Property(e => e.PssRelig)
                .HasMaxLength(255)
                .HasColumnName("PSS_RELIG");
            entity.Property(e => e.PssSchDays)
                .HasMaxLength(255)
                .HasColumnName("PSS_SCH_DAYS");
            entity.Property(e => e.PssSchoolId)
                .HasMaxLength(255)
                .HasColumnName("pss_school_id");
            entity.Property(e => e.PssStdtchRt)
                .HasMaxLength(255)
                .HasColumnName("PSS_STDTCH_RT");
            entity.Property(e => e.PssStuDayHrs)
                .HasMaxLength(255)
                .HasColumnName("PSS_STU_DAY_HRS");
            entity.Property(e => e.PssType)
                .HasMaxLength(255)
                .HasColumnName("PSS_TYPE");
            entity.Property(e => e.PssWhitePct)
                .HasMaxLength(255)
                .HasColumnName("PSS_WHITE_PCT");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(255)
                .HasColumnName("school_name");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.StreetName)
                .HasMaxLength(255)
                .HasColumnName("street_name");
            entity.Property(e => e.Zip)
                .HasMaxLength(255)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<Tbpublicschool>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbpublicschools");

            entity.Property(e => e.Charter)
                .HasMaxLength(50)
                .HasColumnName("charter");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.DistrictId)
                .HasMaxLength(50)
                .HasColumnName("district_id");
            entity.Property(e => e.FreeLunch).HasColumnName("free_lunch");
            entity.Property(e => e.HighGrade)
                .HasMaxLength(50)
                .HasColumnName("high_grade");
            entity.Property(e => e.ImageFile)
                .HasMaxLength(70)
                .HasColumnName("image_file");
            entity.Property(e => e.Lgid).HasColumnName("lgid");
            entity.Property(e => e.LocalCode)
                .HasMaxLength(50)
                .HasColumnName("local_code");
            entity.Property(e => e.Locale)
                .HasMaxLength(50)
                .HasColumnName("locale");
            entity.Property(e => e.LowGrade)
                .HasMaxLength(50)
                .HasColumnName("low_grade");
            entity.Property(e => e.Magnet)
                .HasMaxLength(50)
                .HasColumnName("magnet");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.ReducedLunch).HasColumnName("reduced_lunch");
            entity.Property(e => e.SchoolId)
                .HasMaxLength(50)
                .HasColumnName("school_id");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(100)
                .HasColumnName("school_name");
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasColumnName("state");
            entity.Property(e => e.StateDistrict)
                .HasMaxLength(100)
                .HasColumnName("state_district");
            entity.Property(e => e.StateSchoolId)
                .HasMaxLength(50)
                .HasColumnName("state_school_id");
            entity.Property(e => e.StreetName)
                .HasMaxLength(100)
                .HasColumnName("street_name");
            entity.Property(e => e.StudentTeacherRatio).HasColumnName("student_teacher_ratio");
            entity.Property(e => e.Students).HasColumnName("students");
            entity.Property(e => e.Teachers).HasColumnName("teachers");
            entity.Property(e => e.Title1SchoolWide)
                .HasMaxLength(50)
                .HasColumnName("title_1_school_wide");
            entity.Property(e => e.Titlle1School)
                .HasMaxLength(50)
                .HasColumnName("titlle_1_school");
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasColumnName("zip");
            entity.Property(e => e.Zip4Digit)
                .HasMaxLength(4)
                .HasColumnName("zip_4_digit");
        });

        modelBuilder.Entity<Tbrecentnews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbrecentnews_pkey");

            entity.ToTable("tbrecentnews");

            entity.HasIndex(e => e.Id, "tbrecentnews_ID_key").IsUnique();

            entity.HasIndex(e => e.Id, "tbrecentnews_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.HeaderText)
                .HasMaxLength(150)
                .HasColumnName("header_text");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.NavigateUrl)
                .HasMaxLength(200)
                .HasColumnName("navigate_url");
            entity.Property(e => e.PostingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("posting_date");
            entity.Property(e => e.TextField)
                .HasMaxLength(2000)
                .HasColumnName("text_field");
        });

        modelBuilder.Entity<Tbschooltype>(entity =>
        {
            entity.HasKey(e => e.SchoolTypeId).HasName("tbschooltype_pkey");

            entity.ToTable("tbschooltype");

            entity.HasIndex(e => e.SchoolTypeId, "tbschooltype_SchoolTypeID_key").IsUnique();

            entity.HasIndex(e => e.SchoolTypeId, "tbschooltype_school_type_id_key").IsUnique();

            entity.Property(e => e.SchoolTypeId)
                .ValueGeneratedNever()
                .HasColumnName("school_type_id");
            entity.Property(e => e.SchoolTypeDesc)
                .HasMaxLength(50)
                .HasColumnName("school_type_desc");
        });

        modelBuilder.Entity<Tbsport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbsports_pkey");

            entity.ToTable("tbsports");

            entity.HasIndex(e => e.Id, "tbsports_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tbstate>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("tbstates_pkey");

            entity.ToTable("tbstates");

            entity.HasIndex(e => e.StateId, "tbstates_StateID_key").IsUnique();

            entity.HasIndex(e => e.StateId, "tbstates_state_Id_key").IsUnique();

            entity.HasIndex(e => e.StateId, "tbstates_state_id_key").IsUnique();

            entity.Property(e => e.StateId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("state_id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .HasColumnName("abbreviation");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tbyear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbyears");

            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
