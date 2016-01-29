/*
Navicat MySQL Data Transfer

Source Server         : 19.134.160.157
Source Server Version : 50528
Source Host           : 19.134.160.157:3306
Source Database       : munich_test

Target Server Type    : MYSQL
Target Server Version : 50528
File Encoding         : 65001

Date: 2016-01-29 16:44:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `sys_dept`
-- ----------------------------
DROP TABLE IF EXISTS `sys_dept`;
CREATE TABLE `sys_dept` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DeptCode` varchar(20) CHARACTER SET utf8 NOT NULL,
  `DeptName` varchar(100) CHARACTER SET utf8 NOT NULL,
  `ParentId` int(11) NOT NULL,
  `DeptLever` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_dept
-- ----------------------------
INSERT INTO `sys_dept` VALUES ('11', 'S1', '生产部', '0', '0');
INSERT INTO `sys_dept` VALUES ('12', 'S2', '咨询部', '0', '0');
INSERT INTO `sys_dept` VALUES ('13', 'S3', '总经办', '0', '0');
INSERT INTO `sys_dept` VALUES ('14', 'S1-1', '软件研发部', '11', '0');

-- ----------------------------
-- Table structure for `sys_role`
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_role
-- ----------------------------

-- ----------------------------
-- Table structure for `sys_roletemplate`
-- ----------------------------
DROP TABLE IF EXISTS `sys_roletemplate`;
CREATE TABLE `sys_roletemplate` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) NOT NULL,
  `ActionId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_roletemplate
-- ----------------------------

-- ----------------------------
-- Table structure for `sys_template`
-- ----------------------------
DROP TABLE IF EXISTS `sys_template`;
CREATE TABLE `sys_template` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Vaild` int(2) NOT NULL DEFAULT '1',
  `Lever` int(2) NOT NULL DEFAULT '0',
  `Template` varchar(100) CHARACTER SET utf8 NOT NULL,
  `TemplateUrl` varchar(100) CHARACTER SET utf8 NOT NULL,
  `ParentId` int(11) NOT NULL,
  `TemplateCode` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Type` int(2) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_template
-- ----------------------------
INSERT INTO `sys_template` VALUES ('11', '1', '0', '基础权限', '“”', '0', 'A', '0');
INSERT INTO `sys_template` VALUES ('12', '1', '1', '部门管理', '/Admin/SysDept/Index', '11', 'A1-1', '0');
INSERT INTO `sys_template` VALUES ('13', '1', '0', 'test', '1', '0', '1', '0');
INSERT INTO `sys_template` VALUES ('14', '1', '1', '用户管理', '/Admin/SysUser/Index', '11', 'A1-2', '0');
INSERT INTO `sys_template` VALUES ('15', '1', '1', '角色管理', '/Admin/SysRole/Index', '11', 'A1-3', '0');

-- ----------------------------
-- Table structure for `sys_user`
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RealName` varchar(100) CHARACTER SET utf8 NOT NULL,
  `Account` varchar(100) CHARACTER SET utf8 NOT NULL,
  `Password` varchar(100) CHARACTER SET utf8 NOT NULL,
  `Type` int(2) NOT NULL,
  `Image` blob,
  `Url` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `DeptId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1', 'xiaoyang', '123456', '123', '1', null, null, '1');
INSERT INTO `sys_user` VALUES ('2', 'smallyang', '123', '123', '2', null, null, '3');
INSERT INTO `sys_user` VALUES ('11', 'munich', 'munich', '123456', '0', null, null, '14');
INSERT INTO `sys_user` VALUES ('12', 'smallMunich', 'smallMunich', '123456', '0', null, null, '12');

-- ----------------------------
-- Table structure for `sys_userrole`
-- ----------------------------
DROP TABLE IF EXISTS `sys_userrole`;
CREATE TABLE `sys_userrole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sys_userrole
-- ----------------------------
INSERT INTO `sys_userrole` VALUES ('2', '2', '1');
