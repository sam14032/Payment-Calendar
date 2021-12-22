CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Payments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Payments" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "Amount" REAL NOT NULL,
    "ContactMethod" INTEGER NOT NULL
);

CREATE TABLE "Comments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Comments" PRIMARY KEY AUTOINCREMENT,
    "Text" TEXT NULL,
    CONSTRAINT "FK_Comments_Payments_Id" FOREIGN KEY ("Id") REFERENCES "Payments" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Dates" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Dates" PRIMARY KEY AUTOINCREMENT,
    "NextPaymentDate" TEXT NOT NULL,
    "PaymentFrequency" INTEGER NOT NULL,
    CONSTRAINT "FK_Dates_Payments_Id" FOREIGN KEY ("Id") REFERENCES "Payments" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Payments_Name" ON "Payments" ("Name");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211223080851_initialMigration', '5.0.13');

COMMIT;

