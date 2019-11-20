﻿using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 下发终端升级包
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8108_Formatter))]
    public class JT808_0x8108 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8108>
    {
        /// <summary>
        /// 升级类型
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 5 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 版本号长度
        /// </summary>
        public byte VersionNumLength { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNum { get; set; }
        /// <summary>
        /// 升级数据包长度
        /// </summary>
        public int UpgradePackageLength { get; set; }
        /// <summary>
        /// 升级数据包
        /// </summary>
        public byte[] UpgradePackage { get; set; }

        public JT808_0x8108 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8108 jT808_0X8108 = new JT808_0x8108();
            jT808_0X8108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            jT808_0X8108.MakerId = reader.ReadString(5);
            jT808_0X8108.VersionNumLength = reader.ReadByte();
            jT808_0X8108.VersionNum = reader.ReadString(jT808_0X8108.VersionNumLength);
            jT808_0X8108.UpgradePackageLength = reader.ReadInt32();
            jT808_0X8108.UpgradePackage = reader.ReadArray(jT808_0X8108.UpgradePackageLength).ToArray();
            return jT808_0X8108;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8108 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.UpgradeType);
            writer.WriteString(value.MakerId.PadRight(5, '0'));
            writer.WriteByte((byte)value.VersionNum.Length);
            writer.WriteString(value.VersionNum);
            writer.WriteInt32(value.UpgradePackage.Length);
            writer.WriteArray(value.UpgradePackage);
        }
    }
}
