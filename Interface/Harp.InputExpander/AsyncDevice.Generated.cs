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
        public async Task<AuxiliaryInputs> ReadAuxInPortAsync()
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
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInPortAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInPort.Address));
            return AuxInPort.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInputs> ReadAuxInRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInRisingEdge.Address));
            return AuxInRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInRisingEdge.Address));
            return AuxInRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInRisingEdgeAsync(AuxiliaryInputs value)
        {
            var request = AuxInRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInputs> ReadAuxInFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInFallingEdge.Address));
            return AuxInFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInFallingEdge.Address));
            return AuxInFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInFallingEdgeAsync(AuxiliaryInputs value)
        {
            var request = AuxInFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputPayload> ReadDigitalInputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInput.Address));
            return DigitalInput.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputPayload>> ReadTimestampedDigitalInputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInput.Address));
            return DigitalInput.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInputEnableRisingEdge.Address));
            return DigitalInputEnableRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputEnableRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputEnableRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInputEnableRisingEdge.Address));
            return DigitalInputEnableRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalInputEnableRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalInputEnableRisingEdgeAsync(DigitalInputs value)
        {
            var request = DigitalInputEnableRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInputFallingEdge.Address));
            return DigitalInputFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalInputFallingEdge.Address));
            return DigitalInputFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalInputFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalInputFallingEdgeAsync(DigitalInputs value)
        {
            var request = DigitalInputFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the InputSamplingMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<InputSamplingConfig> ReadInputSamplingModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputSamplingMode.Address));
            return InputSamplingMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InputSamplingMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<InputSamplingConfig>> ReadTimestampedInputSamplingModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputSamplingMode.Address));
            return InputSamplingMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the InputSamplingMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteInputSamplingModeAsync(InputSamplingConfig value)
        {
            var request = InputSamplingMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderSamplingMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderSamplingConfig> ReadEncoderSamplingModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingMode.Address));
            return EncoderSamplingMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderSamplingMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderSamplingConfig>> ReadTimestampedEncoderSamplingModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingMode.Address));
            return EncoderSamplingMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderSamplingMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderSamplingModeAsync(EncoderSamplingConfig value)
        {
            var request = EncoderSamplingMode.FromPayload(MessageType.Write, value);
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
        public async Task<ExpansionBoardType> ReadExpansionBoardAsync()
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
        public async Task<Timestamped<ExpansionBoardType>> ReadTimestampedExpansionBoardAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ExpansionBoard.Address));
            return ExpansionBoard.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ExpansionBoard register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteExpansionBoardAsync(ExpansionBoardType value)
        {
            var request = ExpansionBoard.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
