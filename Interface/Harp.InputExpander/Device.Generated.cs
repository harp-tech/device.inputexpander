using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.InputExpander
{
    /// <summary>
    /// Generates events and processes commands for the InputExpander device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the InputExpander device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="InputExpander"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1106;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(InputExpander);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(AuxInPort) },
            { 33, typeof(AuxInEnableRisingEdge) },
            { 34, typeof(AuxInEnableFallingEdge) },
            { 35, typeof(DigitalInPort) },
            { 36, typeof(DigitalInPortEnableRisingEdge) },
            { 37, typeof(DigitalInPortEnableFallingEdge) },
            { 38, typeof(InputSampling) },
            { 39, typeof(EncoderSampling) },
            { 40, typeof(EncoderData) },
            { 41, typeof(ExpansionBoard) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="InputExpander"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of InputExpander messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="InputExpander"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="InputExpander"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="InputExpander"/> device.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="DigitalInPort"/>
    /// <seealso cref="DigitalInPortEnableRisingEdge"/>
    /// <seealso cref="DigitalInPortEnableFallingEdge"/>
    /// <seealso cref="InputSampling"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(DigitalInPort))]
    [XmlInclude(typeof(DigitalInPortEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInPortEnableFallingEdge))]
    [XmlInclude(typeof(InputSampling))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(EncoderData))]
    [XmlInclude(typeof(ExpansionBoard))]
    [Description("Filters register-specific messages reported by the InputExpander device.")]
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name
        {
            get => $"{nameof(InputExpander)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the InputExpander device.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="DigitalInPort"/>
    /// <seealso cref="DigitalInPortEnableRisingEdge"/>
    /// <seealso cref="DigitalInPortEnableFallingEdge"/>
    /// <seealso cref="InputSampling"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(DigitalInPort))]
    [XmlInclude(typeof(DigitalInPortEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInPortEnableFallingEdge))]
    [XmlInclude(typeof(InputSampling))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(EncoderData))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(TimestampedAuxInPort))]
    [XmlInclude(typeof(TimestampedAuxInEnableRisingEdge))]
    [XmlInclude(typeof(TimestampedAuxInEnableFallingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInPort))]
    [XmlInclude(typeof(TimestampedDigitalInPortEnableRisingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInPortEnableFallingEdge))]
    [XmlInclude(typeof(TimestampedInputSampling))]
    [XmlInclude(typeof(TimestampedEncoderSampling))]
    [XmlInclude(typeof(TimestampedEncoderData))]
    [XmlInclude(typeof(TimestampedExpansionBoard))]
    [Description("Filters and selects specific messages reported by the InputExpander device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// InputExpander register messages.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="DigitalInPort"/>
    /// <seealso cref="DigitalInPortEnableRisingEdge"/>
    /// <seealso cref="DigitalInPortEnableFallingEdge"/>
    /// <seealso cref="InputSampling"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(DigitalInPort))]
    [XmlInclude(typeof(DigitalInPortEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInPortEnableFallingEdge))]
    [XmlInclude(typeof(InputSampling))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(EncoderData))]
    [XmlInclude(typeof(ExpansionBoard))]
    [Description("Formats a sequence of values as specific InputExpander register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that reports the state of the auxiliary inputs.
    /// </summary>
    [Description("Reports the state of the auxiliary inputs.")]
    public partial class AuxInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInPort"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInPort"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInPort"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInPort"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInPort register.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    [Description("Filters and selects timestamped messages from the AuxInPort register.")]
    public partial class TimestampedAuxInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInPort.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInPort.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [Description("Enables rising edge detection on the auxiliary inputs.")]
    public partial class AuxInEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInEnableRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInEnableRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInEnableRisingEdge register.
    /// </summary>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInEnableRisingEdge register.")]
    public partial class TimestampedAuxInEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInEnableRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInEnableRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [Description("Enables falling edge detection on the auxiliary input port.")]
    public partial class AuxInEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInEnableFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInEnableFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInEnableFallingEdge register.
    /// </summary>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInEnableFallingEdge register.")]
    public partial class TimestampedAuxInEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInEnableFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInEnableFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the state of the digital inputs.
    /// </summary>
    [Description("Reports the state of the digital inputs.")]
    public partial class DigitalInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInPort"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInPort"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 2;

        static DigitalInPortPayload ParsePayload(ushort[] payload)
        {
            DigitalInPortPayload result;
            result.DigitalInputState = (DigitalInput)payload[0];
            result.DigitalInputChanged = (DigitalInput)payload[1];
            return result;
        }

        static ushort[] FormatPayload(DigitalInPortPayload value)
        {
            ushort[] result;
            result = new ushort[2];
            result[0] = (ushort)value.DigitalInputState;
            result[1] = (ushort)value.DigitalInputChanged;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInPortPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<ushort>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInPortPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<ushort>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInPort"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPort"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInPortPayload value)
        {
            return HarpMessage.FromUInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInPort"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPort"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInPortPayload value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInPort register.
    /// </summary>
    /// <seealso cref="DigitalInPort"/>
    [Description("Filters and selects timestamped messages from the DigitalInPort register.")]
    public partial class TimestampedDigitalInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInPort.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInPortPayload> GetPayload(HarpMessage message)
        {
            return DigitalInPort.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables rising edge detection on the digital input port.
    /// </summary>
    [Description("Enables rising edge detection on the digital input port.")]
    public partial class DigitalInPortEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPortEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInPortEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInPortEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInPortEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInput GetPayload(HarpMessage message)
        {
            return (DigitalInput)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInPortEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInPortEnableRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPortEnableRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInput value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInPortEnableRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPortEnableRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInput value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInPortEnableRisingEdge register.
    /// </summary>
    /// <seealso cref="DigitalInPortEnableRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DigitalInPortEnableRisingEdge register.")]
    public partial class TimestampedDigitalInPortEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPortEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInPortEnableRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInPortEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInput> GetPayload(HarpMessage message)
        {
            return DigitalInPortEnableRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables falling edge detection on the digital input port.
    /// </summary>
    [Description("Enables falling edge detection on the digital input port.")]
    public partial class DigitalInPortEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPortEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInPortEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInPortEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInPortEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInput GetPayload(HarpMessage message)
        {
            return (DigitalInput)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInPortEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInPortEnableFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPortEnableFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInput value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInPortEnableFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInPortEnableFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInput value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInPortEnableFallingEdge register.
    /// </summary>
    /// <seealso cref="DigitalInPortEnableFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DigitalInPortEnableFallingEdge register.")]
    public partial class TimestampedDigitalInPortEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInPortEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInPortEnableFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInPortEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInput> GetPayload(HarpMessage message)
        {
            return DigitalInPortEnableFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the input sampling mode.
    /// </summary>
    [Description("Configures the input sampling mode.")]
    public partial class InputSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="InputSampling"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="InputSampling"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InputSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputSamplingMode GetPayload(HarpMessage message)
        {
            return (InputSamplingMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InputSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSamplingMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputSamplingMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InputSampling"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSampling"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputSamplingMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InputSampling"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSampling"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputSamplingMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InputSampling register.
    /// </summary>
    /// <seealso cref="InputSampling"/>
    [Description("Filters and selects timestamped messages from the InputSampling register.")]
    public partial class TimestampedInputSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = InputSampling.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InputSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSamplingMode> GetPayload(HarpMessage message)
        {
            return InputSampling.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the rotary encoder sampling mode.
    /// </summary>
    [Description("Configures the rotary encoder sampling mode.")]
    public partial class EncoderSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderSamplingMode GetPayload(HarpMessage message)
        {
            return (EncoderSamplingMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderSamplingMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderSampling"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSampling"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderSamplingMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderSampling"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSampling"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderSamplingMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderSampling register.
    /// </summary>
    /// <seealso cref="EncoderSampling"/>
    [Description("Filters and selects timestamped messages from the EncoderSampling register.")]
    public partial class TimestampedEncoderSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderSampling.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingMode> GetPayload(HarpMessage message)
        {
            return EncoderSampling.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the value of the current read from the rotary encoder.
    /// </summary>
    [Description("Reports the value of the current read from the rotary encoder.")]
    public partial class EncoderData
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderData"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderData"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="EncoderData"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderData"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderData"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderData"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderData"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderData register.
    /// </summary>
    /// <seealso cref="EncoderData"/>
    [Description("Filters and selects timestamped messages from the EncoderData register.")]
    public partial class TimestampedEncoderData
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderData"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderData.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return EncoderData.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [Description("Selects the board to be interfaced with via the expansion port.")]
    public partial class ExpansionBoard
    {
        /// <summary>
        /// Represents the address of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ExpansionBoardTypes GetPayload(HarpMessage message)
        {
            return (ExpansionBoardTypes)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ExpansionBoardTypes> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ExpansionBoardTypes)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ExpansionBoard"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ExpansionBoard"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ExpansionBoardTypes value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ExpansionBoard"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ExpansionBoard"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ExpansionBoardTypes value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ExpansionBoard register.
    /// </summary>
    /// <seealso cref="ExpansionBoard"/>
    [Description("Filters and selects timestamped messages from the ExpansionBoard register.")]
    public partial class TimestampedExpansionBoard
    {
        /// <summary>
        /// Represents the address of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int Address = ExpansionBoard.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ExpansionBoardTypes> GetPayload(HarpMessage message)
        {
            return ExpansionBoard.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// InputExpander device.
    /// </summary>
    /// <seealso cref="CreateAuxInPortPayload"/>
    /// <seealso cref="CreateAuxInEnableRisingEdgePayload"/>
    /// <seealso cref="CreateAuxInEnableFallingEdgePayload"/>
    /// <seealso cref="CreateDigitalInPortPayload"/>
    /// <seealso cref="CreateDigitalInPortEnableRisingEdgePayload"/>
    /// <seealso cref="CreateDigitalInPortEnableFallingEdgePayload"/>
    /// <seealso cref="CreateInputSamplingPayload"/>
    /// <seealso cref="CreateEncoderSamplingPayload"/>
    /// <seealso cref="CreateEncoderDataPayload"/>
    /// <seealso cref="CreateExpansionBoardPayload"/>
    [XmlInclude(typeof(CreateAuxInPortPayload))]
    [XmlInclude(typeof(CreateAuxInEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateAuxInEnableFallingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInPortPayload))]
    [XmlInclude(typeof(CreateDigitalInPortEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInPortEnableFallingEdgePayload))]
    [XmlInclude(typeof(CreateInputSamplingPayload))]
    [XmlInclude(typeof(CreateEncoderSamplingPayload))]
    [XmlInclude(typeof(CreateEncoderDataPayload))]
    [XmlInclude(typeof(CreateExpansionBoardPayload))]
    [Description("Creates standard message payloads for the InputExpander device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateAuxInPortPayload();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the state of the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInPortPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the state of the auxiliary inputs.")]
    public partial class CreateAuxInPortPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the state of the auxiliary inputs.
        /// </summary>
        [Description("The value that reports the state of the auxiliary inputs.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the state of the auxiliary inputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the state of the auxiliary inputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInPort.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInEnableRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables rising edge detection on the auxiliary inputs.")]
    public partial class CreateAuxInEnableRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        [Description("The value that enables rising edge detection on the auxiliary inputs.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInEnableRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [DisplayName("AuxInEnableFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables falling edge detection on the auxiliary input port.")]
    public partial class CreateAuxInEnableFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the auxiliary input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the auxiliary input port.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInEnableFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the state of the digital inputs.
    /// </summary>
    [DisplayName("DigitalInPortPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the state of the digital inputs.")]
    public partial class CreateDigitalInPortPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that reports the state of all digital inputs in the port.
        /// </summary>
        [Description("Reports the state of all digital inputs in the port.")]
        public DigitalInput DigitalInputState { get; set; }

        /// <summary>
        /// Gets or sets a value that reports which digital inputs changed state in the port.
        /// </summary>
        [Description("Reports which digital inputs changed state in the port.")]
        public DigitalInput DigitalInputChanged { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the state of the digital inputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the state of the digital inputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                DigitalInPortPayload value;
                value.DigitalInputState = DigitalInputState;
                value.DigitalInputChanged = DigitalInputChanged;
                return DigitalInPort.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables rising edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInPortEnableRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables rising edge detection on the digital input port.")]
    public partial class CreateDigitalInPortEnableRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables rising edge detection on the digital input port.")]
        public DigitalInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables rising edge detection on the digital input port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables rising edge detection on the digital input port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalInPortEnableRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables falling edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInPortEnableFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables falling edge detection on the digital input port.")]
    public partial class CreateDigitalInPortEnableFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the digital input port.")]
        public DigitalInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables falling edge detection on the digital input port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables falling edge detection on the digital input port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalInPortEnableFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configures the input sampling mode.
    /// </summary>
    [DisplayName("InputSamplingPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configures the input sampling mode.")]
    public partial class CreateInputSamplingPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configures the input sampling mode.
        /// </summary>
        [Description("The value that configures the input sampling mode.")]
        public InputSamplingMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configures the input sampling mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configures the input sampling mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => InputSampling.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configures the rotary encoder sampling mode.
    /// </summary>
    [DisplayName("EncoderSamplingPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configures the rotary encoder sampling mode.")]
    public partial class CreateEncoderSamplingPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configures the rotary encoder sampling mode.
        /// </summary>
        [Description("The value that configures the rotary encoder sampling mode.")]
        public EncoderSamplingMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configures the rotary encoder sampling mode.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configures the rotary encoder sampling mode.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EncoderSampling.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the value of the current read from the rotary encoder.
    /// </summary>
    [DisplayName("EncoderDataPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the value of the current read from the rotary encoder.")]
    public partial class CreateEncoderDataPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the value of the current read from the rotary encoder.
        /// </summary>
        [Description("The value that reports the value of the current read from the rotary encoder.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the value of the current read from the rotary encoder.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the value of the current read from the rotary encoder.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EncoderData.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [DisplayName("ExpansionBoardPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that selects the board to be interfaced with via the expansion port.")]
    public partial class CreateExpansionBoardPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that selects the board to be interfaced with via the expansion port.
        /// </summary>
        [Description("The value that selects the board to be interfaced with via the expansion port.")]
        public ExpansionBoardTypes Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ExpansionBoard.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents the payload of the DigitalInPort register.
    /// </summary>
    public struct DigitalInPortPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalInPortPayload"/> structure.
        /// </summary>
        /// <param name="digitalInputState">Reports the state of all digital inputs in the port.</param>
        /// <param name="digitalInputChanged">Reports which digital inputs changed state in the port.</param>
        public DigitalInPortPayload(
            DigitalInput digitalInputState,
            DigitalInput digitalInputChanged)
        {
            DigitalInputState = digitalInputState;
            DigitalInputChanged = digitalInputChanged;
        }

        /// <summary>
        /// Reports the state of all digital inputs in the port.
        /// </summary>
        public DigitalInput DigitalInputState;

        /// <summary>
        /// Reports which digital inputs changed state in the port.
        /// </summary>
        public DigitalInput DigitalInputChanged;
    }

    /// <summary>
    /// Specifies the state of auxiliary input lines.
    /// </summary>
    [Flags]
    public enum AuxiliaryInput : byte
    {
        Aux0 = 0x1,
        Aux1 = 0x2,
        Aux0Changed = 0x20,
        Aux1Changed = 0x40
    }

    /// <summary>
    /// Specifies the state of the digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInput : ushort
    {
        DI0 = 0x1,
        DI1 = 0x2,
        DI2 = 0x4,
        DI3 = 0x8,
        DI4 = 0x10,
        DI5 = 0x20,
        DI6 = 0x40,
        DI7 = 0x80,
        DI8 = 0x100,
        DI9 = 0x200
    }

    /// <summary>
    /// Specifies the input mode configuration from the available list of options.
    /// </summary>
    public enum InputSamplingMode : byte
    {
        OnInterrupt = 0,
        OnPooling1kHz = 1,
        OnPooling2Hz = 2
    }

    /// <summary>
    /// Specifies the available sampling modes for the rotary encoder.
    /// </summary>
    public enum EncoderSamplingMode : byte
    {
        Disabled = 0,
        Pooling250Hz = 1,
        Pooling500Hz = 2,
        Pooling1kHz = 3,
        OnMovement = 4
    }

    /// <summary>
    /// Specifies the available expansion boards implemented.
    /// </summary>
    public enum ExpansionBoardTypes : byte
    {
        Breakout = 0
    }
}
