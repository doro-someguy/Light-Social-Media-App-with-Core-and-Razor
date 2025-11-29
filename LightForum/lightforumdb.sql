-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gazdă: 127.0.0.1
-- Timp de generare: nov. 28, 2025 la 11:05 AM
-- Versiune server: 10.4.32-MariaDB
-- Versiune PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Bază de date: `lightforumdb`
--

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `reply`
--

CREATE TABLE `reply` (
  `ReplyId` int(11) NOT NULL,
  `ReplyBelongsTo` int(11) NOT NULL,
  `ReplyAuthor` varchar(50) DEFAULT NULL,
  `ReplyContent` text NOT NULL,
  `ReplyCreationDate` datetime NOT NULL,
  `ReplyLikes` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `topic`
--

CREATE TABLE `topic` (
  `TopicId` int(11) NOT NULL,
  `TopicAuthor` varchar(50) DEFAULT NULL,
  `TopicTitle` tinytext NOT NULL,
  `TopicContent` text NOT NULL,
  `TopicCreationDate` datetime NOT NULL,
  `TopicLikes` int(11) NOT NULL,
  `TopicNoReplies` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `user`
--

CREATE TABLE `user` (
  `UserId` int(11) NOT NULL,
  `UserNickname` varchar(50) NOT NULL,
  `UserRegistrationDate` datetime NOT NULL,
  `UserIsVerified` tinyint(1) NOT NULL,
  `UserNoTopics` int(11) NOT NULL,
  `UserNoReplies` int(11) NOT NULL,
  `UserRep` float NOT NULL,
  `UserIsBanned` tinyint(1) NOT NULL,
  `UserIsAdmin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Eliminarea datelor din tabel `user`
--

INSERT INTO `user` (`UserId`, `UserNickname`, `UserRegistrationDate`, `UserIsVerified`, `UserNoTopics`, `UserNoReplies`, `UserRep`, `UserIsBanned`, `UserIsAdmin`) VALUES
(1, 'Dramar1229', '2025-11-28 10:52:19', 1, 1, 1, 0, 0, 1);

--
-- Indexuri pentru tabele eliminate
--

--
-- Indexuri pentru tabele `reply`
--
ALTER TABLE `reply`
  ADD PRIMARY KEY (`ReplyId`),
  ADD UNIQUE KEY `ReplyAuthor` (`ReplyAuthor`),
  ADD KEY `fk_reply_topic` (`ReplyBelongsTo`);

--
-- Indexuri pentru tabele `topic`
--
ALTER TABLE `topic`
  ADD PRIMARY KEY (`TopicId`),
  ADD KEY `fk_topic_author` (`TopicAuthor`);

--
-- Indexuri pentru tabele `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserId`),
  ADD UNIQUE KEY `UserNickname` (`UserNickname`);

--
-- AUTO_INCREMENT pentru tabele eliminate
--

--
-- AUTO_INCREMENT pentru tabele `user`
--
ALTER TABLE `user`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constrângeri pentru tabele eliminate
--

--
-- Constrângeri pentru tabele `reply`
--
ALTER TABLE `reply`
  ADD CONSTRAINT `fk_reply_author` FOREIGN KEY (`ReplyAuthor`) REFERENCES `user` (`UserNickname`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_reply_topic` FOREIGN KEY (`ReplyBelongsTo`) REFERENCES `topic` (`TopicId`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constrângeri pentru tabele `topic`
--
ALTER TABLE `topic`
  ADD CONSTRAINT `fk_topic_author` FOREIGN KEY (`TopicAuthor`) REFERENCES `user` (`UserNickname`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
