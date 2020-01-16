using net_party.Entities.Database;

namespace net_party.Repositories.Sql
{
    public static class TablesSql
    {
        public static readonly string CreateTablesSql = $@"
            CREATE TABLE [dbo].[{nameof(AuthToken)}s](
	            [{nameof(AuthToken.Id)}] [int] IDENTITY(1,1) NOT NULL,
	            [{nameof(AuthToken.AddedDate)}] [date] NOT NULL,
	            [{nameof(AuthToken.Token)}] [nvarchar](max) NOT NULL
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

            CREATE TABLE [dbo].[{nameof(Credential)}s](
	            [{nameof(Credential.Id)}] [int] IDENTITY(1,1) NOT NULL,
	            [{nameof(Credential.Username)}] [nvarchar](max) NOT NULL,
	            [{nameof(Credential.Password)}] [nvarchar](max) NOT NULL
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

            CREATE TABLE [dbo].[{nameof(Server)}s](
	            [{nameof(Server.Id)}] [int] IDENTITY(1,1) NOT NULL,
	            [{nameof(Server.Name)}] [nvarchar](max) NOT NULL,
	            [{nameof(Server.Distance)}] [int] NOT NULL,
             CONSTRAINT [PK_{nameof(Server)}s] PRIMARY KEY CLUSTERED
            (
	            [{nameof(Server.Id)}] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        ";
    }
}