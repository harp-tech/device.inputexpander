#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#define F_CPU 32000000
#include <util/delay.h>

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "InputExpander";

void hwbp_app_initialize(void)
{
    /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 2;
    uint8_t fwH = 2;
    uint8_t fwL = 2;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        1106,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name,
        true,	// This device is able to repeat the harp timestamp clock
        false,	// The device is not able to generate the harp timestamp clock
        0		// Default timestamp offset
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	
}

/************************************************************************/
/* User functions                                                       */
/************************************************************************/
/* Add your functions here or load external functions if needed */

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_define_clock_default(void)
{
	/* Device don't have clock input or output */
}

uint8_t run_only_if_right_hw = true;

#define T_STARTUP_ON  50
#define T_STARTUP_OFF 0

void core_callback_initialize_hardware(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Check if device is an harp input expander hardware */
	if (read_IS_INPUT)
	{
		if (run_only_if_right_hw)
		{
			core_func_catastrophic_error_detected();
		}
		else
		{
			/* Let user know that hardware is wrong by blinking LED0 for 5 seconds */
			for (uint8_t i; i < 50; i++)
			{
				tgl_LED_0;
				_delay_ms(100);
			}
		}
	}
	
	/* Initialize hardware */
	for (uint8_t i = 0; i < 2; i++)
	{
		set_LED_0;  _delay_ms(T_STARTUP_ON); clr_LED_0;  _delay_ms(T_STARTUP_OFF);
		set_LED_1;  _delay_ms(T_STARTUP_ON); clr_LED_1;  _delay_ms(T_STARTUP_OFF);
		set_LED_2;  _delay_ms(T_STARTUP_ON); clr_LED_2;  _delay_ms(T_STARTUP_OFF);
		set_LED_3;  _delay_ms(T_STARTUP_ON); clr_LED_3;  _delay_ms(T_STARTUP_OFF);
		set_LED_4;  _delay_ms(T_STARTUP_ON); clr_LED_4;  _delay_ms(T_STARTUP_OFF);
		set_LED_PWR;  _delay_ms(T_STARTUP_ON); clr_LED_PWR;  _delay_ms(T_STARTUP_OFF);
		set_LED_5;  _delay_ms(T_STARTUP_ON); clr_LED_5;  _delay_ms(T_STARTUP_OFF);
		set_LED_6;  _delay_ms(T_STARTUP_ON); clr_LED_6;  _delay_ms(T_STARTUP_OFF);
		set_LED_7;  _delay_ms(T_STARTUP_ON); clr_LED_7;  _delay_ms(T_STARTUP_OFF);
		set_LED_8;  _delay_ms(T_STARTUP_ON); clr_LED_8;  _delay_ms(T_STARTUP_OFF);
		set_LED_9;  _delay_ms(T_STARTUP_ON); clr_LED_9;  _delay_ms(T_STARTUP_OFF);
		set_LED_STATE;  _delay_ms(T_STARTUP_ON); clr_LED_STATE;  _delay_ms(T_STARTUP_OFF);
	}
	_delay_ms(T_STARTUP_ON*2);

	for (uint8_t i = 0; i < 2; i++)
	{
		set_LED_0; set_LED_1; set_LED_2; set_LED_3; set_LED_4; set_LED_PWR;
		set_LED_5; set_LED_6; set_LED_7; set_LED_8; set_LED_9; set_LED_STATE;
		_delay_ms(T_STARTUP_ON*2);
		
		clr_LED_0; clr_LED_1; clr_LED_2; clr_LED_3; clr_LED_4; clr_LED_PWR;
		clr_LED_5; clr_LED_6; clr_LED_7; clr_LED_8; clr_LED_9; clr_LED_STATE;
		_delay_ms(T_STARTUP_ON*2);
	}
	
	_delay_ms(500);
	set_LED_PWR;
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	app_regs.REG_AUX_INPUTS_RISING_EDGE_ENABLE = B_AUX_IN0 | B_AUX_IN1;
	app_regs.REG_AUX_INPUTS_FALLING_EDGE_ENABLE = B_AUX_IN0 | B_AUX_IN1;
	
	app_regs.REG_RISING_EDGE_ENABLE = B_IN0 | B_IN1 | B_IN2 | B_IN3 | B_IN4 | B_IN5 | B_IN6 | B_IN7 | B_IN8 | B_IN9;
	app_regs.REG_FALLING_EDGE_ENABLE = B_IN0 | B_IN1 | B_IN2 | B_IN3 | B_IN4 | B_IN5 | B_IN6 | B_IN7 | B_IN8 | B_IN9;
}

extern void check_for_event_and_update_LEDs(void);

void core_callback_registers_were_reinitialized(void)
{
	check_for_event_and_update_LEDs();
	
	app_write_REG_INPUT_MODE(&app_regs.REG_INPUT_MODE);
	
	app_write_REG_EXPANSION_OPTIONS(&app_regs.REG_EXPANSION_OPTIONS);
	
	
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update visual indicators */
	switch (app_regs.REG_EXPANSION_OPTIONS)
	{
		case MSK_BREAKOUT:
			PORTH_OUT = (uint8_t) app_regs.REG_INPUTS[0];
			if (app_regs.REG_INPUTS[0] & B_IN8) { set_LED_8; } else { clr_LED_8; }
			if (app_regs.REG_INPUTS[0] & B_IN9) { set_LED_9; } else { clr_LED_9; }
			break;
	}

	set_LED_PWR;
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
	clr_LED_0; clr_LED_1; clr_LED_2; clr_LED_3; clr_LED_4;
	clr_LED_5; clr_LED_6; clr_LED_7; clr_LED_8; clr_LED_9;
	clr_LED_PWR;
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
uint16_t acquisition_counter = 0;

void core_callback_t_before_exec(void)
{
	acquisition_counter++;
	
	if (app_regs.REG_ENCODER_MODE & GM_ENC_MODE)
	{
		if ((app_regs.REG_ENCODER_MODE == MSK_ENC_1000Hz && ((acquisition_counter & 1) == 0)) ||
			(app_regs.REG_ENCODER_MODE == MSK_ENC_500Hz  && ((acquisition_counter & 3) == 0)) ||
			(app_regs.REG_ENCODER_MODE == MSK_ENC_250Hz  && ((acquisition_counter & 7) == 0)))
		{
	
			int16_t timer_cnt = TCE1_CNT;
	
			if (timer_cnt > 32768)
			{
				app_regs.REG_ENCODER = 0xFFFF - timer_cnt;
			}
			else
			{
				app_regs.REG_ENCODER = (32768 - timer_cnt) * -1;
			}
			core_func_send_event(ADD_REG_ENCODER, true);
		}
	
		if (app_regs.REG_ENCODER_MODE == MSK_ENC_WHEN_CHANGE && ((acquisition_counter & 1) == 0))
		{
			int16_t timer_cnt = TCE1_CNT;
		
			if (timer_cnt > 32768)
			{
				timer_cnt = 0xFFFF - timer_cnt;
			
				if (timer_cnt != app_regs.REG_ENCODER)
				{
					app_regs.REG_ENCODER = timer_cnt;
					core_func_send_event(ADD_REG_ENCODER, true);
				}
			}
			else
			{
				timer_cnt = (32768 - timer_cnt) * -1;
			
				if (timer_cnt != app_regs.REG_ENCODER)
				{
					app_regs.REG_ENCODER = timer_cnt;
					core_func_send_event(ADD_REG_ENCODER, true);
				}
			}
		}
	}
	
	if ((app_regs.REG_INPUT_MODE == MSK_AT_2000FPS) || ((app_regs.REG_INPUT_MODE == MSK_AT_1000FPS) && ((acquisition_counter & 1) == 0)))
	{
		app_regs.REG_INPUTS[0]  = (read_IN0 ? B_IN0 : 0) | (read_IN1 ? B_IN1 : 0) | (read_IN2 ? B_IN2 : 0) | (read_IN3 ? B_IN3 : 0) | (read_IN4 ? B_IN4 : 0);
		app_regs.REG_INPUTS[0] |= (read_IN5 ? B_IN5 : 0) | (read_IN6 ? B_IN6 : 0) | (read_IN7 ? B_IN7 : 0) | (read_IN8 ? B_IN8 : 0) | (read_IN9 ? B_IN9 : 0);
		core_func_send_event(ADD_REG_INPUTS, true);
		
		/* Update LEDs */
		if (core_bool_is_visual_enabled())
		{
			PORTH_OUT = (uint8_t) app_regs.REG_INPUTS[0];
			if (app_regs.REG_INPUTS[0] & B_IN8) { set_LED_8; } else { clr_LED_8; }
			if (app_regs.REG_INPUTS[0] & B_IN9) { set_LED_9; } else { clr_LED_9; }
		}
	}
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void)
{
	acquisition_counter = 0;
}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void) {}


/************************************************************************/
/* Callbacks: clock control                                             */
/************************************************************************/
void core_callback_clock_to_repeater(void) {}
void core_callback_clock_to_generator(void) {}
void core_callback_clock_to_unlock(void) {}
void core_callback_clock_to_lock(void) {}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}