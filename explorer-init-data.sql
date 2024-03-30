INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-1, 'admin@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 0, true, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-11, 'autor1@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 1, true, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-12, 'autor2@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 1, true, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-13, 'autor3@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 1, true, true);

INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-21, 'turista1@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 2, true, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-22, 'turista2@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 2, true, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "IsVerified")
VALUES (-23, 'turista3@gmail.com', '9834876dcfb05cb167a5c24953eba58c4ac89b1adf57f28f2f9d09af107ee8f0', 2, true, true);

INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-11, -11, 'Ana', 'Anić', 'autor1@gmail.com', 'slika1.jpg', 'prva bio', 'prvi moto');
INSERT INTO stakeholders."People"(
     "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-12, -12, 'Lena', 'Lenić', 'autor2@gmail.com', 'slika2.jpg', 'druga bio', 'drugi moto');
INSERT INTO stakeholders."People"(
     "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-13, -13, 'Sara', 'Sarić', 'autor3@gmail.com', 'slika3.jpg', 'treci bio', 'treci moto');

INSERT INTO stakeholders."People"(
     "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-21, -21, 'Pera', 'Perić', 'turista1@gmail.com', 'slika4.jpg', 'turista1 bio', 'turista1 moto');
INSERT INTO stakeholders."People"(
     "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-22, -22, 'Mika', 'Mikić', 'turista2@gmail.com', 'slika5.jpg', 'turista2 bio', 'turista2 moto');
INSERT INTO stakeholders."People"(
     "Id", "UserId", "Name", "Surname", "Email", "ProfilePictureUrl", "Biography", "Motto")
VALUES (-23, -23, 'Steva', 'Stević', 'turista3@gmail.com', 'slika6.jpg', 'turista3 bio', 'turista3 moto');