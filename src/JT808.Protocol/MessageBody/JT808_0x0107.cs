﻿using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端属性应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0107_Formatter))]
    public class JT808_0x0107 : JT808Bodies,IJT808MessagePackFormatter<JT808_0x0107>
    {
        /// <summary>
        /// 终端类型
        /// bit0，0：不适用客运车辆，1：适用客运车辆；
        /// bit1，0：不适用危险品车辆，1：适用危险品车辆；
        /// bit2，0：不适用普通货运车辆，1：适用普通货运车辆；
        /// bit3，0：不适用出租车辆，1：适用出租车辆；
        /// bit6，0：不支持硬盘录像，1：支持硬盘录像；
        /// bit7，0：一体机，1：分体机
        /// </summary>
        public ushort TerminalType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 5 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 终端型号
        /// BYTE[20]
        /// 20 个字节，此终端型号由制造商自行定义，位数不足时，后补“0X00”。
        /// </summary>
        public string TerminalModel { get; set; }
        /// <summary>
        /// 终端ID 
        /// BYTE[7]
        /// 7 个字节，由大写字母和数字组成，此终端 ID 由制造商自行定义，位数不足时，后补“0X00”
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// 终端 SIM 卡 ICCID 
        /// BCD[10]
        /// </summary>
        public string Terminal_SIM_ICCID { get; set; }
        /// <summary>
        /// 终端硬件版本号长度
        /// </summary>
        public byte Terminal_Hardware_Version_Length { get; set; }
        /// <summary>
        /// 终端硬件版本号
        /// </summary>
        public string Terminal_Hardware_Version_Num { get; set; }
        /// <summary>
        /// 终端固件版本号长度
        /// </summary>
        public byte Terminal_Firmware_Version_Length { get; set; }
        /// <summary>
        /// 终端固件版本号
        /// </summary>
        public string Terminal_Firmware_Version_Num { get; set; }
        /// <summary>
        /// GNSS 模块属性
        /// bit0，0：不支持 GPS 定位， 1：支持 GPS 定位；
        /// bit1，0：不支持北斗定位， 1：支持北斗定位；
        /// bit2，0：不支持 GLONASS 定位， 1：支持 GLONASS 定位；
        /// bit3，0：不支持 Galileo 定位， 1：支持 Galileo 定位
        /// </summary>
        public byte GNSSModule { get; set; }
        /// <summary>
        /// 通信模块属性
        /// bit0，0：不支持GPRS通信， 1：支持GPRS通信；
        /// bit1，0：不支持CDMA通信， 1：支持CDMA通信；
        /// bit2，0：不支持TD-SCDMA通信， 1：支持TD-SCDMA通信；
        /// bit3，0：不支持WCDMA通信， 1：支持WCDMA通信；
        /// bit4，0：不支持CDMA2000通信， 1：支持CDMA2000通信。
        /// bit5，0：不支持TD-LTE通信， 1：支持TD-LTE通信；
        /// bit7，0：不支持其他通信方式， 1：支持其他通信方式
        /// </summary>
        public byte CommunicationModule { get; set; }

        public JT808_0x0107 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = reader.ReadUInt16();
            jT808_0X0107.MakerId = reader.ReadString(5);
            jT808_0X0107.TerminalModel = reader.ReadString(20);
            jT808_0X0107.TerminalId = reader.ReadString(7);
            jT808_0X0107.Terminal_SIM_ICCID = reader.ReadBCD(10, config.Trim);
            jT808_0X0107.Terminal_Hardware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Hardware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Length = reader.ReadByte();
            jT808_0X0107.Terminal_Firmware_Version_Num = reader.ReadString(jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.GNSSModule = reader.ReadByte();
            jT808_0X0107.CommunicationModule = reader.ReadByte();
            return jT808_0X0107;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0107 value, IJT808Config config)
        {
            writer.WriteUInt16(value.TerminalType);
            writer.WriteString(value.MakerId.PadRight(5, '0'));
            writer.WriteString(value.TerminalModel.PadRight(20, '0'));
            writer.WriteString(value.TerminalId.PadRight(7, '0'));
            writer.WriteBCD(value.Terminal_SIM_ICCID, 10);
            writer.WriteByte((byte)value.Terminal_Hardware_Version_Num.Length);
            writer.WriteString(value.Terminal_Hardware_Version_Num);
            writer.WriteByte((byte)value.Terminal_Firmware_Version_Num.Length);
            writer.WriteString(value.Terminal_Firmware_Version_Num);
            writer.WriteByte(value.GNSSModule);
            writer.WriteByte(value.CommunicationModule);
        }
    }
}
