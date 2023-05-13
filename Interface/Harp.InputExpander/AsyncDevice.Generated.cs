using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.InputExpander
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with InputExpander devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a InputExpander device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with InputExpander devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInPort register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInput> ReadAuxInPortAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInPort.Address));
            return AuxInPort.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInPort register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInput>> ReadTimestampedAuxInPortAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInPort.Address));
            return AuxInPort.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInput> ReadAuxInEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInEnableRisingEdge.Address));
            return AuxInEnableRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInput>> ReadTimestampedAuxInEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInEnableRisingEdge.Address));
            return AuxInEnableRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInEnableRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInEnableRisingEdgeAsync(AuxiliaryInput value)
        {
            var request = AuxInEnableRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInEnableFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInput> ReadAuxInEnableFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInEnableFallingEdge.Address));
            return AuxInEnableFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInEnableFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInput>> ReadTimestampedAuxInEnableFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInEnableFallingEdge.Address));
            return AuxInEnableFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInEnableFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInEnableFallingEdgeAsync(AuxiliaryInput value)
        {
            var request = AuxInEnableFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInPort register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInPortPayload> ReadDigitalInPortAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPort.Address));
            return DigitalInPort.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInPort register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInPortPayload>> ReadTimestampedDigitalInPortAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPort.Address));
            return DigitalInPort.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInPortEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInput> ReadDigitalInPortEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPortEnableRisingEdge.Address));
            return DigitalInPortEnableRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInPortEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInput>> ReadTimestampedDigitalInPortEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPortEnableRisingEdge.Address));
            return DigitalInPortEnableRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalInPortEnableRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalInPortEnableRisingEdgeAsync(DigitalInput value)
        {
            var request = DigitalInPortEnableRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInPortEnableFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInput> ReadDigitalInPortEnableFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPortEnableFallingEdge.Address));
            return DigitalInPortEnableFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInPortEnableFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInput>> ReadTimestampedDigitalInPortEnableFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInPortEnableFallingEdge.Address));
            return DigitalInPortEnableFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalInPortEnableFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalInPortEnableFallingEdgeAsync(DigitalInput value)
        {
            var request = DigitalInPortEnableFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the InputSampling register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputSamplingMode> ReadInputSamplingAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputSampling.Address));
            return InputSampling.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InputSampling register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputSamplingMode>> ReadTimestampedInputSamplingAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputSampling.Address));
            return InputSampling.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the InputSampling register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInputSamplingAsync(InputSamplingMode value)
        {
            var request = InputSampling.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderSampling register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderSamplingMode> ReadEncoderSamplingAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSampling.Address));
            return EncoderSampling.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderSampling register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderSamplingMode>> ReadTimestampedEncoderSamplingAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSampling.Address));
            return EncoderSampling.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderSampling register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderSamplingAsync(EncoderSamplingMode value)
        {
            var request = EncoderSampling.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadEncoderDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(EncoderData.Address));
            return EncoderData.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedEncoderDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(EncoderData.Address));
            return EncoderData.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ExpansionBoard register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ExpansionBoardTypes> ReadExpansionBoardAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ExpansionBoard.Address));
            return ExpansionBoard.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ExpansionBoard register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ExpansionBoardTypes>> ReadTimestampedExpansionBoardAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ExpansionBoard.Address));
            return ExpansionBoard.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ExpansionBoard register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteExpansionBoardAsync(ExpansionBoardTypes value)
        {
            var request = ExpansionBoard.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
