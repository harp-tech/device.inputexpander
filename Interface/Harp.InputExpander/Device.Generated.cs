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
            { 32, typeof(AuxInState) },
            { 33, typeof(AuxInRisingEdge) },
            { 34, typeof(AuxInFallingEdge) },
            { 35, typeof(DigitalInput) },
            { 36, typeof(DigitalInputEnableRisingEdge) },
            { 37, typeof(DigitalInputFallingEdge) },
            { 38, typeof(InputSampleMode) },
            { 39, typeof(EncoderMode) },
            { 40, typeof(Encoder) },
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
    /// <seealso cref="AuxInState"/>
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSampleMode"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInState))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSampleMode))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(ExpansionBoard))]
    [Description("Filters register-specific messages reported by the InputExpander device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new AuxInState();
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
    /// <seealso cref="AuxInState"/>
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSampleMode"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInState))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSampleMode))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(TimestampedAuxInState))]
    [XmlInclude(typeof(TimestampedAuxInRisingEdge))]
    [XmlInclude(typeof(TimestampedAuxInFallingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInput))]
    [XmlInclude(typeof(TimestampedDigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(TimestampedDigitalInputFallingEdge))]
    [XmlInclude(typeof(TimestampedInputSampleMode))]
    [XmlInclude(typeof(TimestampedEncoderMode))]
    [XmlInclude(typeof(TimestampedEncoder))]
    [XmlInclude(typeof(TimestampedExpansionBoard))]
    [Description("Filters and selects specific messages reported by the InputExpander device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new AuxInState();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// InputExpander register messages.
    /// </summary>
    /// <seealso cref="AuxInState"/>
    /// <seealso cref="AuxInRisingEdge"/>
    /// <seealso cref="AuxInFallingEdge"/>
    /// <seealso cref="DigitalInput"/>
    /// <seealso cref="DigitalInputEnableRisingEdge"/>
    /// <seealso cref="DigitalInputFallingEdge"/>
    /// <seealso cref="InputSampleMode"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="ExpansionBoard"/>
    [XmlInclude(typeof(AuxInState))]
    [XmlInclude(typeof(AuxInRisingEdge))]
    [XmlInclude(typeof(AuxInFallingEdge))]
    [XmlInclude(typeof(DigitalInput))]
    [XmlInclude(typeof(DigitalInputEnableRisingEdge))]
    [XmlInclude(typeof(DigitalInputFallingEdge))]
    [XmlInclude(typeof(InputSampleMode))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(ExpansionBoard))]
    [Description("Formats a sequence of values as specific InputExpander register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new AuxInState();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that reports the state of the auxiliary inputs.
    /// </summary>
    [Description("Reports the state of the auxiliary inputs.")]
    public partial class AuxInState
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInState"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInputs GetPayload(HarpMessage message)
        {
            return (AuxiliaryInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInState register.
    /// </summary>
    /// <seealso cref="AuxInState"/>
    [Description("Filters and selects timestamped messages from the AuxInState register.")]
    public partial class TimestampedAuxInState
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInState"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInputs> GetPayload(HarpMessage message)
        {
            return AuxInState.GetTimestampedPayload(message);
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
    /// Represents a register that configures the input sample mode.
    /// </summary>
    [Description("Configures the input sample mode.")]
    public partial class InputSampleMode
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSampleMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="InputSampleMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="InputSampleMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InputSampleMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static InputSampleModeConfig GetPayload(HarpMessage message)
        {
            return (InputSampleModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InputSampleMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSampleModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((InputSampleModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InputSampleMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSampleMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, InputSampleModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InputSampleMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputSampleMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, InputSampleModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InputSampleMode register.
    /// </summary>
    /// <seealso cref="InputSampleMode"/>
    [Description("Filters and selects timestamped messages from the InputSampleMode register.")]
    public partial class TimestampedInputSampleMode
    {
        /// <summary>
        /// Represents the address of the <see cref="InputSampleMode"/> register. This field is constant.
        /// </summary>
        public const int Address = InputSampleMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InputSampleMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<InputSampleModeConfig> GetPayload(HarpMessage message)
        {
            return InputSampleMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the rotary encoder acquisition mode.
    /// </summary>
    [Description("Configures the rotary encoder acquisition mode.")]
    public partial class EncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        static EncoderModePayload ParsePayload(byte payload)
        {
            EncoderModePayload result;
            result.SampleRate = (EncoderSampleRate)(byte)(payload & 0x7);
            result.Mode = (EncoderModeConfig)(byte)((payload & 0x8) >> 3);
            return result;
        }

        static byte FormatPayload(EncoderModePayload value)
        {
            byte result;
            result = (byte)((byte)value.SampleRate & 0x7);
            result |= (byte)(((byte)value.Mode << 3) & 0x8);
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderModePayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadByte());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModePayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderModePayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderModePayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderMode register.
    /// </summary>
    /// <seealso cref="EncoderMode"/>
    [Description("Filters and selects timestamped messages from the EncoderMode register.")]
    public partial class TimestampedEncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModePayload> GetPayload(HarpMessage message)
        {
            return EncoderMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the value of the latest read from the rotary encoder.
    /// </summary>
    [Description("Reports the value of the latest read from the rotary encoder.")]
    public partial class Encoder
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Encoder"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoder"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Encoder"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoder"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Encoder register.
    /// </summary>
    /// <seealso cref="Encoder"/>
    [Description("Filters and selects timestamped messages from the Encoder register.")]
    public partial class TimestampedEncoder
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int Address = Encoder.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return Encoder.GetTimestampedPayload(message);
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
    /// <seealso cref="CreateAuxInStatePayload"/>
    /// <seealso cref="CreateAuxInRisingEdgePayload"/>
    /// <seealso cref="CreateAuxInFallingEdgePayload"/>
    /// <seealso cref="CreateDigitalInputPayload"/>
    /// <seealso cref="CreateDigitalInputEnableRisingEdgePayload"/>
    /// <seealso cref="CreateDigitalInputFallingEdgePayload"/>
    /// <seealso cref="CreateInputSampleModePayload"/>
    /// <seealso cref="CreateEncoderModePayload"/>
    /// <seealso cref="CreateEncoderPayload"/>
    /// <seealso cref="CreateExpansionBoardPayload"/>
    [XmlInclude(typeof(CreateAuxInStatePayload))]
    [XmlInclude(typeof(CreateAuxInRisingEdgePayload))]
    [XmlInclude(typeof(CreateAuxInFallingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInputPayload))]
    [XmlInclude(typeof(CreateDigitalInputEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateDigitalInputFallingEdgePayload))]
    [XmlInclude(typeof(CreateInputSampleModePayload))]
    [XmlInclude(typeof(CreateEncoderModePayload))]
    [XmlInclude(typeof(CreateEncoderPayload))]
    [XmlInclude(typeof(CreateExpansionBoardPayload))]
    [XmlInclude(typeof(CreateTimestampedAuxInStatePayload))]
    [XmlInclude(typeof(CreateTimestampedAuxInRisingEdgePayload))]
    [XmlInclude(typeof(CreateTimestampedAuxInFallingEdgePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputFallingEdgePayload))]
    [XmlInclude(typeof(CreateTimestampedInputSampleModePayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderModePayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderPayload))]
    [XmlInclude(typeof(CreateTimestampedExpansionBoardPayload))]
    [Description("Creates standard message payloads for the InputExpander device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateAuxInStatePayload();
        }

        string INamedElement.Name => $"{nameof(InputExpander)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reports the state of the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInStatePayload")]
    [Description("Creates a message payload that reports the state of the auxiliary inputs.")]
    public partial class CreateAuxInStatePayload
    {
        /// <summary>
        /// Gets or sets the value that reports the state of the auxiliary inputs.
        /// </summary>
        [Description("The value that reports the state of the auxiliary inputs.")]
        public AuxiliaryInputs AuxInState { get; set; }

        /// <summary>
        /// Creates a message payload for the AuxInState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AuxiliaryInputs GetPayload()
        {
            return AuxInState;
        }

        /// <summary>
        /// Creates a message that reports the state of the auxiliary inputs.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AuxInState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.AuxInState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reports the state of the auxiliary inputs.
    /// </summary>
    [DisplayName("TimestampedAuxInStatePayload")]
    [Description("Creates a timestamped message payload that reports the state of the auxiliary inputs.")]
    public partial class CreateTimestampedAuxInStatePayload : CreateAuxInStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that reports the state of the auxiliary inputs.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AuxInState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.AuxInState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInRisingEdgePayload")]
    [Description("Creates a message payload that enables rising edge detection on the auxiliary inputs.")]
    public partial class CreateAuxInRisingEdgePayload
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        [Description("The value that enables rising edge detection on the auxiliary inputs.")]
        public AuxiliaryInputs AuxInRisingEdge { get; set; }

        /// <summary>
        /// Creates a message payload for the AuxInRisingEdge register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AuxiliaryInputs GetPayload()
        {
            return AuxInRisingEdge;
        }

        /// <summary>
        /// Creates a message that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AuxInRisingEdge register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.AuxInRisingEdge.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [DisplayName("TimestampedAuxInRisingEdgePayload")]
    [Description("Creates a timestamped message payload that enables rising edge detection on the auxiliary inputs.")]
    public partial class CreateTimestampedAuxInRisingEdgePayload : CreateAuxInRisingEdgePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AuxInRisingEdge register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.AuxInRisingEdge.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [DisplayName("AuxInFallingEdgePayload")]
    [Description("Creates a message payload that enables falling edge detection on the auxiliary input port.")]
    public partial class CreateAuxInFallingEdgePayload
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the auxiliary input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the auxiliary input port.")]
        public AuxiliaryInputs AuxInFallingEdge { get; set; }

        /// <summary>
        /// Creates a message payload for the AuxInFallingEdge register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AuxiliaryInputs GetPayload()
        {
            return AuxInFallingEdge;
        }

        /// <summary>
        /// Creates a message that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AuxInFallingEdge register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.AuxInFallingEdge.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [DisplayName("TimestampedAuxInFallingEdgePayload")]
    [Description("Creates a timestamped message payload that enables falling edge detection on the auxiliary input port.")]
    public partial class CreateTimestampedAuxInFallingEdgePayload : CreateAuxInFallingEdgePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AuxInFallingEdge register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.AuxInFallingEdge.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reports the state of the digital inputs.
    /// </summary>
    [DisplayName("DigitalInputPayload")]
    [Description("Creates a message payload that reports the state of the digital inputs.")]
    public partial class CreateDigitalInputPayload
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
        /// Creates a message payload for the DigitalInput register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputPayload GetPayload()
        {
            DigitalInputPayload value;
            value.DigitalInputState = DigitalInputState;
            value.DigitalInputChanged = DigitalInputChanged;
            return value;
        }

        /// <summary>
        /// Creates a message that reports the state of the digital inputs.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInput register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.DigitalInput.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reports the state of the digital inputs.
    /// </summary>
    [DisplayName("TimestampedDigitalInputPayload")]
    [Description("Creates a timestamped message payload that reports the state of the digital inputs.")]
    public partial class CreateTimestampedDigitalInputPayload : CreateDigitalInputPayload
    {
        /// <summary>
        /// Creates a timestamped message that reports the state of the digital inputs.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInput register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.DigitalInput.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables rising edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInputEnableRisingEdgePayload")]
    [Description("Creates a message payload that enables rising edge detection on the digital input port.")]
    public partial class CreateDigitalInputEnableRisingEdgePayload
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables rising edge detection on the digital input port.")]
        public DigitalInputs DigitalInputEnableRisingEdge { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputEnableRisingEdge register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputEnableRisingEdge;
        }

        /// <summary>
        /// Creates a message that enables rising edge detection on the digital input port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputEnableRisingEdge register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.DigitalInputEnableRisingEdge.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables rising edge detection on the digital input port.
    /// </summary>
    [DisplayName("TimestampedDigitalInputEnableRisingEdgePayload")]
    [Description("Creates a timestamped message payload that enables rising edge detection on the digital input port.")]
    public partial class CreateTimestampedDigitalInputEnableRisingEdgePayload : CreateDigitalInputEnableRisingEdgePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables rising edge detection on the digital input port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputEnableRisingEdge register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.DigitalInputEnableRisingEdge.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables falling edge detection on the digital input port.
    /// </summary>
    [DisplayName("DigitalInputFallingEdgePayload")]
    [Description("Creates a message payload that enables falling edge detection on the digital input port.")]
    public partial class CreateDigitalInputFallingEdgePayload
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the digital input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the digital input port.")]
        public DigitalInputs DigitalInputFallingEdge { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputFallingEdge register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputFallingEdge;
        }

        /// <summary>
        /// Creates a message that enables falling edge detection on the digital input port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputFallingEdge register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.DigitalInputFallingEdge.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables falling edge detection on the digital input port.
    /// </summary>
    [DisplayName("TimestampedDigitalInputFallingEdgePayload")]
    [Description("Creates a timestamped message payload that enables falling edge detection on the digital input port.")]
    public partial class CreateTimestampedDigitalInputFallingEdgePayload : CreateDigitalInputFallingEdgePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables falling edge detection on the digital input port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputFallingEdge register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.DigitalInputFallingEdge.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the input sample mode.
    /// </summary>
    [DisplayName("InputSampleModePayload")]
    [Description("Creates a message payload that configures the input sample mode.")]
    public partial class CreateInputSampleModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the input sample mode.
        /// </summary>
        [Description("The value that configures the input sample mode.")]
        public InputSampleModeConfig InputSampleMode { get; set; }

        /// <summary>
        /// Creates a message payload for the InputSampleMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public InputSampleModeConfig GetPayload()
        {
            return InputSampleMode;
        }

        /// <summary>
        /// Creates a message that configures the input sample mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the InputSampleMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.InputSampleMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the input sample mode.
    /// </summary>
    [DisplayName("TimestampedInputSampleModePayload")]
    [Description("Creates a timestamped message payload that configures the input sample mode.")]
    public partial class CreateTimestampedInputSampleModePayload : CreateInputSampleModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the input sample mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the InputSampleMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.InputSampleMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the rotary encoder acquisition mode.
    /// </summary>
    [DisplayName("EncoderModePayload")]
    [Description("Creates a message payload that configures the rotary encoder acquisition mode.")]
    public partial class CreateEncoderModePayload
    {
        /// <summary>
        /// Gets or sets a value that sets the sample rate of the Encoder.
        /// </summary>
        [Description("Sets the sample rate of the Encoder.")]
        public EncoderSampleRate SampleRate { get; set; }

        /// <summary>
        /// Gets or sets a value that sets the acquisition mode of the Encoder.
        /// </summary>
        [Description("Sets the acquisition mode of the Encoder.")]
        public EncoderModeConfig Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderModePayload GetPayload()
        {
            EncoderModePayload value;
            value.SampleRate = SampleRate;
            value.Mode = Mode;
            return value;
        }

        /// <summary>
        /// Creates a message that configures the rotary encoder acquisition mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.EncoderMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the rotary encoder acquisition mode.
    /// </summary>
    [DisplayName("TimestampedEncoderModePayload")]
    [Description("Creates a timestamped message payload that configures the rotary encoder acquisition mode.")]
    public partial class CreateTimestampedEncoderModePayload : CreateEncoderModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the rotary encoder acquisition mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.EncoderMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reports the value of the latest read from the rotary encoder.
    /// </summary>
    [DisplayName("EncoderPayload")]
    [Description("Creates a message payload that reports the value of the latest read from the rotary encoder.")]
    public partial class CreateEncoderPayload
    {
        /// <summary>
        /// Gets or sets the value that reports the value of the latest read from the rotary encoder.
        /// </summary>
        [Description("The value that reports the value of the latest read from the rotary encoder.")]
        public short Encoder { get; set; }

        /// <summary>
        /// Creates a message payload for the Encoder register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return Encoder;
        }

        /// <summary>
        /// Creates a message that reports the value of the latest read from the rotary encoder.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Encoder register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.Encoder.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reports the value of the latest read from the rotary encoder.
    /// </summary>
    [DisplayName("TimestampedEncoderPayload")]
    [Description("Creates a timestamped message payload that reports the value of the latest read from the rotary encoder.")]
    public partial class CreateTimestampedEncoderPayload : CreateEncoderPayload
    {
        /// <summary>
        /// Creates a timestamped message that reports the value of the latest read from the rotary encoder.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Encoder register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.Encoder.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [DisplayName("ExpansionBoardPayload")]
    [Description("Creates a message payload that selects the board to be interfaced with via the expansion port.")]
    public partial class CreateExpansionBoardPayload
    {
        /// <summary>
        /// Gets or sets the value that selects the board to be interfaced with via the expansion port.
        /// </summary>
        [Description("The value that selects the board to be interfaced with via the expansion port.")]
        public ExpansionBoardType ExpansionBoard { get; set; }

        /// <summary>
        /// Creates a message payload for the ExpansionBoard register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ExpansionBoardType GetPayload()
        {
            return ExpansionBoard;
        }

        /// <summary>
        /// Creates a message that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ExpansionBoard register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.InputExpander.ExpansionBoard.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [DisplayName("TimestampedExpansionBoardPayload")]
    [Description("Creates a timestamped message payload that selects the board to be interfaced with via the expansion port.")]
    public partial class CreateTimestampedExpansionBoardPayload : CreateExpansionBoardPayload
    {
        /// <summary>
        /// Creates a timestamped message that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ExpansionBoard register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.InputExpander.ExpansionBoard.FromPayload(timestamp, messageType, GetPayload());
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
    /// Represents the payload of the EncoderMode register.
    /// </summary>
    public struct EncoderModePayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncoderModePayload"/> structure.
        /// </summary>
        /// <param name="sampleRate">Sets the sample rate of the Encoder.</param>
        /// <param name="mode">Sets the acquisition mode of the Encoder.</param>
        public EncoderModePayload(
            EncoderSampleRate sampleRate,
            EncoderModeConfig mode)
        {
            SampleRate = sampleRate;
            Mode = mode;
        }

        /// <summary>
        /// Sets the sample rate of the Encoder.
        /// </summary>
        public EncoderSampleRate SampleRate;

        /// <summary>
        /// Sets the acquisition mode of the Encoder.
        /// </summary>
        public EncoderModeConfig Mode;
    }

    /// <summary>
    /// Specifies the state of auxiliary input lines.
    /// </summary>
    [Flags]
    public enum AuxiliaryInputs : byte
    {
        None = 0x0,
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
        None = 0x0,
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
    public enum InputSampleModeConfig : byte
    {
        OnInterrupt = 0,
        SampleRate1kHz = 1,
        SampleRate2kHz = 2
    }

    /// <summary>
    /// Specifies the available sample modes for the rotary encoder.
    /// </summary>
    public enum EncoderSampleRate : byte
    {
        Disabled = 0,
        SampleRate250Hz = 1,
        SampleRate500Hz = 2,
        SampleRate1kHz = 3,
        OnMovement = 4
    }

    /// <summary>
    /// Specifies the available acquisition modes for the rotary encoder.
    /// </summary>
    public enum EncoderModeConfig : byte
    {
        Position = 0,
        Displacement = 1
    }

    /// <summary>
    /// Specifies the available expansion boards implemented.
    /// </summary>
    public enum ExpansionBoardType : byte
    {
        Breakout = 0
    }
}
