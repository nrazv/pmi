CREATE TABLE ProjectsInfo (
    Id          TEXT NOT NULL CONSTRAINT PK_ProjectsInfo PRIMARY KEY,
    Name        TEXT NOT NULL,
    CreatedDate TEXT NOT NULL,
    LastUpdated TEXT,
    Status      TEXT NOT NULL
);



CREATE TABLE Projects (
    Id            TEXT NOT NULL CONSTRAINT PK_Projects PRIMARY KEY,
    Name          TEXT NOT NULL,
    DomainName    TEXT,
    IpAddress     TEXT,
    ProjectInfoId TEXT,
    CONSTRAINT FK_Projects_ProjectsInfo_ProjectInfoId FOREIGN KEY (ProjectInfoId)
    REFERENCES ProjectsInfo (Id) 
);

