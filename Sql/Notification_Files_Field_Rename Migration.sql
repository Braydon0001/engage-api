START TRANSACTION;

ALTER TABLE `Notifications` RENAME COLUMN `featuredImage` TO `Files`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230327155224_Notification_Files_Field_Rename', '7.0.0');

COMMIT;

