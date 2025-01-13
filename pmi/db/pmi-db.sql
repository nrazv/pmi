create table ProjectInfo(
    Id varchar(255) PRIMARY KEY,
    Name varchar(255) unique not null,
    CreatedDate DATETIME not null,
    LastUpdated DATETIME,
    Status varchar(255) not null
);


create table Projects(
    Id varchar(255) PRIMARY KEY,
    Name varchar(255) unique not null,
    DomainName varchar(255),
    IpAddress varchar(255),
    FOREIGN KEY (Id) REFERENCES ProjectInfo(Id)
);


create table ExecutedTools(
    Id varchar(255) PRIMARY KEY,
    Name varchar(255) not null,
    ExecutionResult text,
    ExecutedDate DATETIME,
    ProjectId varchar(255),
    FOREIGN KEY(ProjectId) REFERENCES Projects(Id)
);