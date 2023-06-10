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
            { 33, typeof(AuxInRisingEdge) },
            { 34, typeof(AuxInFallingEdge) },
            { 35, typeof(DigitalInput) },
            { 36, typeof(DigitalInputEnableRisingEdge) },
            { 37, typeof(DigitalInputFallingEdge) },
            { 38, typeof(InputSamplingMode) },
            { 39, typeof(EncoderSamplingMode) },
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
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSamplingMode"/>
    /// <seealso cref="EncoderSamplingMode"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSamplingMode))]
    [XmlInclude(typeof(EncoderSamplingMode))]
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
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSamplingMode"/>
    /// <seealso cref="EncoderSamplingMode"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSamplingMode))]
    [XmlInclude(typeof(EncoderSamplingMode))]
    [XmlInclude(typeof(EncoderData))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(TimestampedAuxInPort))]
    [XmlInclude(typeof(TimestampedAuxInRisingEdge))]
    [XmlInclude(typeof(TimestampedAuxInFallingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInput))]
    [XmlInclude(typeof(TimestampedDigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInputFallingEdge))]
    [XmlInclude(typeof(TimestampedInputSamplingMode))]
    [XmlInclude(typeof(TimestampedEncoderSamplingMode))]
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
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSamplingMode"/>
    /// <seealso cref="EncoderSamplingMode"/>
    /// <seealso cref="EncoderData"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSamplingMode))]
    [XmlInclude(typeof(EncoderSamplingMode))]
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
        public static AuxiliaryInputs GetPayload(HarpMessage message)
        {
            return (AuxiliaryInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInputs)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInputs value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInputs value)
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
        public static Timestamped<AuxiliaryInputs> GetPayload(HarpMessage message)
        {
            return AuxInPort.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [Description("Enables rising edge detection on the auxiliary inputs.")]
    public partial class AuxInRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInputs GetPayload(HarpMessage message)
        {
            return (AuxiliaryInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInRisingEdge register.
    /// </summary>
    /// <seealso cref="AuxInRisingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInRisingEdge register.")]
    public partial class TimestampedAuxInRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetPayload(HarpMessage message)
        {
            return AuxInRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [Description("Enables falling edge detection on the auxiliary input port.")]
    public partial class AuxInFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInputs GetPayload(HarpMessage message)
        {
            return (AuxiliaryInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInFallingEdge register.
    /// </summary>
    /// <seealso cref="AuxInFallingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInFallingEdge register.")]
    public partial class TimestampedAuxInFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetPayload(HarpMessage message)
        {
            return AuxInFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the state of the digital inputs.
    /// </summary>
    [Description("Reports the state of the digital inputs.")]
    public partial class DigitalInput
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInput"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInput"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInput"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 2;

        static DigitalInputPayload ParsePayload(ushort[] payload)
        {
            DigitalInputPayload result;
            result.DigitalInputState = (DigitalInputs)payload[0];
            result.DigitalInputChanged = (DigitalInputs)payload[1];
            return result;
        }

        static ushort[] FormatPayload(DigitalInputPayload value)
        {
            ushort[] result;
            result = new ushort[2];
            result[0] = (ushort)value.DigitalInputState;
            result[1] = (ushort)value.DigitalInputChanged;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<ushort>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<ushort>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInput"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInput"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputPayload value)
        {
            return HarpMessage.FromUInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInput"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInput"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputPayload value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInput register.
    /// </summary>
    /// <seealso cref="DigitalInput"/>
    [Description("Filters and selects timestamped messages from the DigitalInput register.")]
    public partial class TimestampedDigitalInput
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInput"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInput.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputPayload> GetPayload(HarpMessage message)
        {
            return DigitalInput.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables rising edge detection on the digital input port.
    /// </summary>
    [Description("Enables rising edge detection on the digital input port.")]
    public partial class DigitalInputEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputEnableRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputEnableRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputEnableRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputEnableRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputEnableRisingEdge register.
    /// </summary>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DigitalInputEnableRisingEdge register.")]
    public partial class TimestampedDigitalInputEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputEnableRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputEnableRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables falling edge detection on the digital input port.
    /// </summary>
    [Description("Enables falling edge detection on the digital input port.")]
    public partial class DigitalInputFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputFallingEdge register.
    /// </summary>
    /// <seealso cref="DigitalInputFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DigitalInputFallingEdge register.")]
    public partial class TimestampedDigitalInputFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the input sampling mode.
    /// </summary>
    [Description("Configures the input sampling mode.")]
    public partial class InputSamplingMode
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="InputSamplingMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="InputSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InputSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputSamplingConfig GetPayload(HarpMessage message)
        {
            return (InputSamplingConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InputSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSamplingConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputSamplingConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InputSamplingMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSamplingMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputSamplingConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InputSamplingMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSamplingMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputSamplingConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InputSamplingMode register.
    /// </summary>
    /// <seealso cref="InputSamplingMode"/>
    [Description("Filters and selects timestamped messages from the InputSamplingMode register.")]
    public partial class TimestampedInputSamplingMode
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int Address = InputSamplingMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InputSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSamplingConfig> GetPayload(HarpMessage message)
        {
            return InputSamplingMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the rotary encoder sampling mode.
    /// </summary>
    [Description("Configures the rotary encoder sampling mode.")]
    public partial class EncoderSamplingMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderSamplingMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderSamplingConfig GetPayload(HarpMessage message)
        {
            return (EncoderSamplingConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderSamplingConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderSamplingMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSamplingMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderSamplingConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderSamplingMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSamplingMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderSamplingConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderSamplingMode register.
    /// </summary>
    /// <seealso cref="EncoderSamplingMode"/>
    [Description("Filters and selects timestamped messages from the EncoderSamplingMode register.")]
    public partial class TimestampedEncoderSamplingMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSamplingMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderSamplingMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderSamplingMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingConfig> GetPayload(HarpMessage message)
        {
            return EncoderSamplingMode.GetTimestampedPayload(message);
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
        public static ExpansionBoardType GetPayload(HarpMessage message)
        {
            return (ExpansionBoardType)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ExpansionBoardType> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ExpansionBoardType)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, ExpansionBoardType value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ExpansionBoardType value)
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
        public static Timestamped<ExpansionBoardType> GetPayload(HarpMessage message)
        {
            return ExpansionBoard.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// InputExpander device.
    /// </summary>
    /// <seealso cref="CreateAuxInPortPayload"/>
    /// <seealso cref="CreateAuxInRisingEdgePayload"/>
    /// <seealso cref="CreateAuxInFallingEdgePayload"/>
    /// <seealso cref="CreateDigitalInputPayload"/>
    /// <seealso cref="CreateDigitalInputEnableRisingEdgePayload"/>
    /// <seealso cref="CreateDigitalInputFallingEdgePayload"/>
    /// <seealso cref="CreateInputSamplingModePayload"/>
    /// <seealso cref="CreateEncoderSamplingModePayload"/>
    /// <seealso cref="CreateEncoderDataPayload"/>
    /// <seealso cref="CreateExpansionBoardPayload"/>
    [XmlInclude(typeof(CreateAuxInPortPayload))]
    [XmlInclude(typeof(CreateAuxInRisingEdgePayload))]
    [XmlInclude(typeof(CreateAuxInFallingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInputPayload))]
    [XmlInclude(typeof(CreateDigitalInputEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInputFallingEdgePayload))]
    [XmlInclude(typeof(CreateInputSamplingModePayload))]
    [XmlInclude(typeof(CreateEncoderSamplingModePayload))]
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
        public AuxiliaryInputs Value { get; set; }

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
    [DisplayName("AuxInRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables rising edge detection on the auxiliary inputs.")]
    public partial class CreateAuxInRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        [Description("The value that enables rising edge detection on the auxiliary inputs.")]
        public AuxiliaryInputs Value { get; set; }

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
            return source.Select(_ => AuxInRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [DisplayName("AuxInFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables falling edge detection on the auxiliary input port.")]
    public partial class CreateAuxInFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the auxiliary input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the auxiliary input port.")]
        public AuxiliaryInputs Value { get; set; }

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
            return source.Select(_ => AuxInFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the state of the digital inputs.
    /// </summary>
    [DisplayName("DigitalInputPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the state of the digital inputs.")]
    public partial class CreateDigitalInputPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that reports the state of all digital inputs in the port.
        /// </summary>
        [Description("Reports the state of all digital inputs in the port.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Gets or sets a value that reports which digital inputs changed state in the port.
        /// </summary>
        [Description("Reports which digital inputs changed state in the port.")]
        public DigitalInputs DigitalInputChanged { get; set; }

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
                DigitalInputPayload value;
                value.DigitalInputState = DigitalInputState;
                value.DigitalInputChanged = DigitalInputChanged;
                return DigitalInput.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables rising edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInputEnableRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables rising edge detection on the digital input port.")]
    public partial class CreateDigitalInputEnableRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables rising edge detection on the digital input port.")]
        public DigitalInputs Value { get; set; }

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
            return source.Select(_ => DigitalInputEnableRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables falling edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInputFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables falling edge detection on the digital input port.")]
    public partial class CreateDigitalInputFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the digital input port.")]
        public DigitalInputs Value { get; set; }

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
            return source.Select(_ => DigitalInputFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configures the input sampling mode.
    /// </summary>
    [DisplayName("InputSamplingModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configures the input sampling mode.")]
    public partial class CreateInputSamplingModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configures the input sampling mode.
        /// </summary>
        [Description("The value that configures the input sampling mode.")]
        public InputSamplingConfig Value { get; set; }

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
            return source.Select(_ => InputSamplingMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configures the rotary encoder sampling mode.
    /// </summary>
    [DisplayName("EncoderSamplingModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configures the rotary encoder sampling mode.")]
    public partial class CreateEncoderSamplingModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configures the rotary encoder sampling mode.
        /// </summary>
        [Description("The value that configures the rotary encoder sampling mode.")]
        public EncoderSamplingConfig Value { get; set; }

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
            return source.Select(_ => EncoderSamplingMode.FromPayload(MessageType, Value));
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
        public ExpansionBoardType Value { get; set; }

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
    /// Represents the payload of the DigitalInput register.
    /// </summary>
    public struct DigitalInputPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalInputPayload"/> structure.
        /// </summary>
        /// <param name="digitalInputState">Reports the state of all digital inputs in the port.</param>
        /// <param name="digitalInputChanged">Reports which digital inputs changed state in the port.</param>
        public DigitalInputPayload(
            DigitalInputs digitalInputState,
            DigitalInputs digitalInputChanged)
        {
            DigitalInputState = digitalInputState;
            DigitalInputChanged = digitalInputChanged;
        }

        /// <summary>
        /// Reports the state of all digital inputs in the port.
        /// </summary>
        public DigitalInputs DigitalInputState;

        /// <summary>
        /// Reports which digital inputs changed state in the port.
        /// </summary>
        public DigitalInputs DigitalInputChanged;
    }

    /// <summary>
    /// Specifies the state of auxiliary input lines.
    /// </summary>
    [Flags]
    public enum AuxiliaryInputs : byte
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
    public enum DigitalInputs : ushort
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
    public enum InputSamplingConfig : byte
    {
        OnInterrupt = 0,
        OnPooling1kHz = 1,
        OnPooling2Hz = 2
    }

    /// <summary>
    /// Specifies the available sampling modes for the rotary encoder.
    /// </summary>
    public enum EncoderSamplingConfig : byte
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
    public enum ExpansionBoardType : byte
    {
        Breakout = 0
    }
}
