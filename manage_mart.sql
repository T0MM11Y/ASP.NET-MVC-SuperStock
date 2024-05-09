-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 09, 2024 at 03:47 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `manage_mart`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Description` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`Id`, `Name`, `Description`) VALUES
(1, 'Bahan Pokok', 'Beras, mie instan, tepung, gula, minyak goreng, telur, daging, sayur, dan buah'),
(2, 'Produk Segar', 'Susu, yoghurt, roti, kue, telur, daging segar, sayur, dan buah\r\n'),
(3, 'Snack & Minuman	', 'Snack, keripik, biskuit, cokelat, permen, minuman ringan, jus, teh, dan kopi\r\n'),
(4, 'Perlengkapan Rumah Tangga	', 'Sabun mandi, sampo, pasta gigi, deterjen, sabun cuci piring, tisu, dan alat kebersihan lainnya');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `SellerId` int(11) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL,
  `ExpiredAt` datetime(6) NOT NULL,
  `UrlPhoto` longtext NOT NULL,
  `Stock` int(11) NOT NULL,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`Id`, `Name`, `CategoryId`, `SellerId`, `CreatedAt`, `UpdatedAt`, `ExpiredAt`, `UrlPhoto`, `Stock`, `Price`) VALUES
(1, 'Beras Pulen 5 Kg', 1, 1, '2024-05-10 03:13:20.625436', '0001-01-01 00:00:00.000000', '2025-08-31 00:00:00.000000', '/products/beras.jfif', 10, 60000),
(2, 'Indomie Rasa Kari', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', '2029-10-30 00:00:00.000000', '/products/Mie Instan Rasa Kari.jfif', 20, 3000),
(3, 'Tepung Terigu Protein ', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', '2026-02-27 00:00:00.000000', '/products/Tepung Terigu Protein Tinggi.jfif', 15, 12500),
(4, 'Gula Pasir Putih', 1, 1, '2024-05-10 03:20:03.466184', '0001-01-01 00:00:00.000000', '2028-03-11 00:00:00.000000', '/products/Gula Pasir Putih.jfif', 25, 10000),
(5, 'Minyak Goreng Sawit', 1, 1, '2024-05-10 03:20:46.497189', '0001-01-01 00:00:00.000000', '2027-09-30 00:00:00.000000', '/products/Minyak Goreng Sawit.jfif', 12, 15000),
(6, 'Telur Ayam Ras', 1, 1, '2024-05-10 03:21:58.144697', '0001-01-01 00:00:00.000000', '2025-05-20 00:00:00.000000', '/products/Telur Ayam Ras.jfif', 30, 20000),
(7, 'Daging Ayam Broiler', 1, 1, '2024-05-10 03:23:33.291524', '0001-01-01 00:00:00.000000', '2025-05-15 00:00:00.000000', '/products/Daging Ayam Broiler.jfif', 10, 25000),
(8, 'Bayam Hijau', 1, 1, '2024-05-10 03:24:14.983332', '0001-01-01 00:00:00.000000', '2025-12-05 00:00:00.000000', '/products/Bayam Hijau.jfif', 20, 10000),
(9, 'Pisang Cavendish', 1, 1, '2024-05-10 03:24:58.182477', '0001-01-01 00:00:00.000000', '2025-02-01 00:00:00.000000', '/products/Pisang Cavendish.jfif', 25, 15000),
(10, 'Susu UHT Full Cream	', 2, 2, '2024-05-10 03:27:03.463335', '0001-01-01 00:00:00.000000', '2025-06-30 00:00:00.000000', '/products/Susu UHT Full Cream.jfif', 15, 9000),
(11, 'Yoghurt Rasa Stroberi', 2, 2, '2024-05-10 03:27:46.620240', '0001-01-01 00:00:00.000000', '2025-05-22 00:00:00.000000', '/products/Yoghurt Rasa Stroberi.jfif', 10, 7000),
(12, 'Roti Tawar Gandum', 2, 2, '2024-05-10 03:28:33.063871', '0001-01-01 00:00:00.000000', '2025-12-18 00:00:00.000000', '/products/Roti Tawar Gandum.jfif', 20, 8000),
(13, 'Kue Bolu Susu', 2, 2, '2024-05-10 03:29:08.330377', '0001-01-01 00:00:00.000000', '2025-06-17 00:00:00.000000', '/products/Kue Bolu Susu.jfif', 15, 15000),
(14, 'Telur Ayam Kampung', 2, 2, '2024-05-10 03:31:38.062179', '0001-01-01 00:00:00.000000', '2025-05-24 00:00:00.000000', '/products/Telur Ayam Kampung.jfif', 10, 10000),
(15, 'Apel Fuji', 2, 2, '2024-05-10 03:32:26.802599', '0001-01-01 00:00:00.000000', '2025-07-28 00:00:00.000000', '/products/Apel Fuji.jfif', 12, 20000),
(16, 'Chiz Crackers Rasa Keju', 3, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', '2028-12-23 00:00:00.000000', '/products/Chiz Crackers Rasa Keju.jfif', 31, 5000),
(17, 'Cokelat Batang Susu	', 3, 2, '2024-05-10 03:34:37.056133', '0001-01-01 00:00:00.000000', '2029-12-31 00:00:00.000000', '/products/Cokelat Batang Susu.jfif', 27, 12000),
(18, 'Permen Jelly', 3, 2, '2024-05-10 03:35:09.175781', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', '/products/Permen Jelly.jfif', 25, 5000),
(19, 'Coca Cola', 3, 2, '2024-05-10 03:36:18.205495', '0001-01-01 00:00:00.000000', '2024-05-15 00:00:00.000000', '/products/CocaCola.jfif', 19, 5000),
(20, 'Sabun Mandi Cair Antiseptik', 4, 5, '2024-05-10 03:38:44.238900', '0001-01-01 00:00:00.000000', '2026-07-31 00:00:00.000000', '/products/Sabun Mandi Cair Antiseptik.jfif', 15, 10000),
(21, 'Sampo Anti Ketombe', 4, 5, '2024-05-10 03:39:24.979259', '0001-01-01 00:00:00.000000', '2028-06-14 00:00:00.000000', '/products/Sampo Anti Ketombe.jfif', 10, 12000),
(22, 'Deterjen Bubuk', 4, 5, '2024-05-10 03:40:07.212432', '0001-01-01 00:00:00.000000', '2026-11-19 00:00:00.000000', '/products/Deterjen Bubuk.jfif', 16, 15000),
(23, 'Pembersih Lantai', 1, 5, '2024-05-10 03:40:50.270427', '0001-01-01 00:00:00.000000', '2027-10-11 00:00:00.000000', '/products/Pembersih Lantai.jfif', 15, 12000),
(24, 'Sabun Cuci Piring', 4, 5, '2024-05-10 03:41:43.656909', '0001-01-01 00:00:00.000000', '2029-11-21 00:00:00.000000', '/products/Sabun Cuci Piring.jfif', 15, 7000);

-- --------------------------------------------------------

--
-- Table structure for table `sellers`
--

CREATE TABLE `sellers` (
  `Id` int(11) NOT NULL,
  `UserId` longtext NOT NULL,
  `Name` longtext NOT NULL,
  `Username` longtext NOT NULL,
  `Address` longtext NOT NULL,
  `Phone` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Role` longtext NOT NULL,
  `UrlPhoto` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `sellers`
--

INSERT INTO `sellers` (`Id`, `UserId`, `Name`, `Username`, `Address`, `Phone`, `Password`, `Role`, `UrlPhoto`, `CreatedAt`, `UpdatedAt`) VALUES
(1, '902afa73-a0a1-4bfa-ac31-53c2b512a57c', 'Budi Setiawan	', 'budisetiawan', 'Jalan Anggrek No. 1, RT/RW 01/02, Kelurahan Melati, Kecamatan Medan Baru, Kota Medan, Sumatera Utara 20151', '+628123456789', 'seller123', 'seller', '/upload/vibrent_27.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(2, '158c0f46-5a5b-4f04-b4fd-cc8b507104e8', 'Ani Lestari', 'anilestari', 'Jalan Cempaka No. 2, RT/RW 03/04, Kelurahan Sari Rejo, Kecamatan Medan Polonia, Kota Medan, Sumatera Utara 20111', '+628123456780', 'seller123', 'seller', '/upload/vibrent_26.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(3, 'a91070db-ca4f-4b31-b9e8-42058f34c462', 'Candra Pratama', 'candrapratama', 'Jalan Dahlia No. 3, RT/RW 05/06, Kelurahan Binjai, Kecamatan Medan Timur, Kota Medan, Sumatera Utara 20142', '+628123456781', 'seller123', 'seller', '/upload/vibrent_25.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(4, '5ac2c356-86e5-43a8-b027-496dbe08463b', 'Dini Rahmawati', 'dinirhmawati', 'Jalan Mawar No. 4, RT/RW 07/08, Kelurahan Medan Timur, Kecamatan Medan Timur, Kota Medan, Sumatera Utara 20131', '+628123456782', 'seller123', 'seller', '/upload/vibrent_24.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(5, '9358ac6c-76d3-4b54-aaf3-57ad94c4214d', 'Eka Saputra', 'ekasaputra', 'Jalan Melati No. 5, RT/RW 09/10, Kelurahan Medan Barat, Kecamatan Medan Barat, Kota Medan, Sumatera Utara 20121', '+628123456783', 'seller123', 'seller', '/upload/vibrent_23.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(6, 'd5c153b9-94e1-4852-9b12-7bb94f5df78e', 'Fitriani', 'fitriani', 'Jalan Anggrek No. 6, RT/RW 11/12, Kelurahan Medan Helvetia, Kecamatan Medan Helvetia, Kota Medan, Sumatera Utara 20161', '+628123456784', 'seller123', 'seller', '/upload/vibrent_22.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(7, '60e972bb-5a0e-4813-a29e-39ea205137b9', 'Galih Permana', 'galihpermana', 'Jalan Cempaka No. 7, RT/RW 13/14, Kelurahan Medan Polonia, Kecamatan Medan Polonia, Kota Medan, Sumatera Utara 20111', '+628123456785', 'seller123', 'seller', '/upload/vibrent_21.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(8, '798e9eec-2541-433f-acf7-482d4c9f2956', 'Hana Sari', 'hanasari', 'Jalan Dahlia No. 8, RT/RW 15/16, Kelurahan Binjai, Kecamatan Medan Timur, Kota Medan, Sumatera Utara 20142', '+628123456786', 'seller123', 'seller', '/upload/vibrent_20.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(9, '55dd0732-26c8-4b7a-a298-2b5e086fbe38', 'Indah Permata', 'indahpermata', 'Jalan Mawar No. 9, RT/RW 17/18, Kelurahan Medan Timur, Kecamatan Medan Timur, Kota Medan, Sumatera Utara 20131', '+628123456787', 'seller123', 'seller', '/upload/vibrent_19.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(10, 'e8aaccee-d231-4fe1-8b9c-c6ca4bc5477e', 'Joko Susanto', 'jokosusanto', 'Jalan Melati No. 10, RT/RW 19/20, Kelurahan Medan Barat, Kecamatan Medan Barat, Kota Medan, Sumatera Utara 20121', '+628123456788', 'seller123', 'seller', '/upload/upstream_16.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(11, 'fde1e46e-c231-4a0f-8865-3519bd27d3a8', 'Karina Dewi', 'karinadewi', 'Jalan Anggrek No. 11, RT/RW 21/22, Kelurahan Medan Helvetia, Kecamatan Medan Helvetia, Kota Medan, Sumatera Utara 20161', '+628123456789', 'seller123', 'seller', '/upload/teams_4.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000'),
(12, '3124ce9f-de9e-4219-b9b2-cffaccf6a79e', 'Lintang Mentari', 'lintangmentari', 'Jalan Cempaka No. 12, RT/RW 23/24, Kelurahan Medan Polonia, Kecamatan Medan Polonia, Kota Medan, Sumatera Utara 20111', '+628123456790', 'seller123', 'seller', '/upload/teams_1.png', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `Username` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Role` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `Username`, `Password`, `Role`) VALUES
(1, 'admin', 'admin', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240501202009_t0mm11y', '8.0.4');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`),
  ADD KEY `IX_Products_SellerId` (`SellerId`);

--
-- Indexes for table `sellers`
--
ALTER TABLE `sellers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `sellers`
--
ALTER TABLE `sellers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `FK_Products_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Products_Sellers_SellerId` FOREIGN KEY (`SellerId`) REFERENCES `sellers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
