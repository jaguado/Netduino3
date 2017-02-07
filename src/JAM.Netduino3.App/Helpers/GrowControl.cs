using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Net.NetworkInformation;

namespace JAM.Netduino3.App.Helpers
{
    /// <summary>
    /// 8-Relay Shield temporizer
    /// </summary>
    public class GrowControl
    {
        // ReSharper disable once ConvertToConstant.Local
        private readonly bool _cancel = false;
        private const int RelayTimeout = 5000;

        private readonly Cpu.Pin _relayPin1;
        private readonly Cpu.Pin _relayPin2;
        private readonly Cpu.Pin _relayPîn3;
        private readonly Cpu.Pin _relayPin4;
        private readonly Cpu.Pin _relayPin5;
        private readonly Cpu.Pin _relayPin6;
        private readonly Cpu.Pin _relayPin7;
        private readonly Cpu.Pin _relayPin8;

        private OutputPort _relay1;
        private OutputPort _relay2;
        private OutputPort _relay3;
        private OutputPort _relay4;
        private OutputPort _relay5;
        private OutputPort _relay6;
        private OutputPort _relay7;
        private OutputPort _relay8;

        private GrowControlConfig _config;
        private int _days = 0;

        internal NetworkInterface _ni = null;
        internal string _apiServer = "";

        public GrowControl(NetworkInterface NI, string ApiServer)
        {
            _ni = NI;
            _apiServer = ApiServer;
            _relayPin1 = Pins.GPIO_PIN_D7;
            _relayPin2 = Pins.GPIO_PIN_D6;
            _relayPîn3 = Pins.GPIO_PIN_D5;
            _relayPin4 = Pins.GPIO_PIN_D4;
            _relayPin5 = Pins.GPIO_PIN_D3;
            _relayPin6 = Pins.GPIO_PIN_D2;
            _relayPin7 = Pins.GPIO_PIN_D1;
            _relayPin8 = Pins.GPIO_PIN_D0;
        }

        public GrowControl(NetworkInterface NI, string ApiServer, Cpu.Pin relay1, Cpu.Pin relay2, Cpu.Pin relay3, Cpu.Pin relay4,
                           Cpu.Pin relay5, Cpu.Pin relay6, Cpu.Pin relay7, Cpu.Pin relay8)
        {
            _ni = NI;
            _apiServer = ApiServer;
            _relayPin1 = relay1;
            _relayPin2 = relay2;
            _relayPîn3 = relay3;
            _relayPin4 = relay4;
            _relayPin5 = relay5;
            _relayPin6 = relay6;
            _relayPin7 = relay7;
            _relayPin8 = relay8;
        }

        public void RunOnSeparateThread()
        {
            var thControl = new Thread(StartRelayControl);
            try
            {
                thControl.Start();
                IotRegistration(); //Register on cloud service
            }
            catch (Exception e)
            {
                var log = string.Concat(e.Message, "|", e.StackTrace);
                Log.Print(log);
                thControl.Abort();
                throw;
            }
        }
        public void ChangeRelayState(int index, bool state)
        {
            OutputPort obj = null;
            switch (index)
            {
                case 1:
                    obj = _relay1;
                    break;
                case 2:
                    obj = _relay2;
                    break;
                case 3:
                    obj = _relay3;
                    break;
                case 4:
                    obj = _relay4;
                    break;
                case 5:
                    obj = _relay5;
                    break;
                case 6:
                    obj = _relay6;
                    break;
                case 7:
                    obj = _relay7;
                    break;
                case 8:
                    obj = _relay8;
                    break;
            }
            if (obj != null)
                obj.Write(!state);

            Debug.Print("Relay n° " + index + " Value set to:" + state);
        }

        public bool GetRelayState(int index)
        {
            OutputPort obj = null;
            switch (index)
            {
                case 1:
                    obj = _relay1;
                    break;
                case 2:
                    obj = _relay2;
                    break;
                case 3:
                    obj = _relay3;
                    break;
                case 4:
                    obj = _relay4;
                    break;
                case 5:
                    obj = _relay5;
                    break;
                case 6:
                    obj = _relay6;
                    break;
                case 7:
                    obj = _relay7;
                    break;
                case 8:
                    obj = _relay8;
                    break;
            }
            bool state = false;
            if (obj != null)
                state = obj.Read();

            Debug.Print("Relay n° " + index + " Value is" + state);
            return state;
        }

        public bool[] GetRelaysState()
        {
            var res = new bool[RelaysCount+1];
            for(int i = 1; i <= RelaysCount; i++)
            {
                res[i] = !GetRelayState(i);
            }
            return res;
        }

        public int RelaysCount
        {
            get
            {
                return 8;
            }
        }

        internal void StartRelayControl()
        {
            _config = GetConfig();
            if (_config != null)
            {
                var config1 = _config.RelayConfiguration[0] as GrowControlConfig.RelayConfig;
                if (config1 != null && config1.Enabled)
                    if (_relay1 == null)
                        _relay1 = new OutputPort(_relayPin1, !config1.StartValue);

                var config2 = _config.RelayConfiguration[1] as GrowControlConfig.RelayConfig;
                if (config2 != null && config2.Enabled)
                    if (_relay2 == null)
                        _relay2 = new OutputPort(_relayPin2, !config2.StartValue);

                var config3 = _config.RelayConfiguration[2] as GrowControlConfig.RelayConfig;
                if (config3 != null && config3.Enabled)
                    if (_relay3 == null)
                        _relay3 = new OutputPort(_relayPîn3, !config3.StartValue);

                var config4 = _config.RelayConfiguration[3] as GrowControlConfig.RelayConfig;
                if (config4 != null && config4.Enabled)
                    if (_relay4 == null)
                        _relay4 = new OutputPort(_relayPin4, !config4.StartValue);

                var config5 = _config.RelayConfiguration[4] as GrowControlConfig.RelayConfig;
                if (config5 != null && config5.Enabled)
                    if (_relay5 == null)
                        _relay5 = new OutputPort(_relayPin5, !config5.StartValue);

                var config6 = _config.RelayConfiguration[5] as GrowControlConfig.RelayConfig;
                if (config6 != null && config6.Enabled)
                    if (_relay6 == null)
                        _relay6 = new OutputPort(_relayPin6, !config6.StartValue);

                var config7 = _config.RelayConfiguration[6] as GrowControlConfig.RelayConfig;
                if (config7 != null && config7.Enabled)
                    if (_relay7 == null)
                        _relay7 = new OutputPort(_relayPin7, !config7.StartValue);

                var config8 = _config.RelayConfiguration[7] as GrowControlConfig.RelayConfig;
                if (config8 != null && config8.Enabled)
                    if (_relay8 == null)
                        _relay8 = new OutputPort(_relayPin8, !config8.StartValue);
            }

            Debug.Print("Memory: " + Debug.GC(true));


            //First Queue
            _queueList = new ArrayList
            {
                new DictionaryEntry("1",new ArrayList
                {
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0 //Lights  18h on, 6h off
                 }),
                 new DictionaryEntry("4",new ArrayList
                {
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0, //Lights 18h on, 6h off
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0 //Lights  18h on, 6h off
                 }),
                 new DictionaryEntry("2",new ArrayList
                {
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                }),
                 new DictionaryEntry("5",new ArrayList
                {
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                })
                //}),
                //new DictionaryEntry("3",new ArrayList
                //{
                //    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                //}),
                // new DictionaryEntry("6",new ArrayList
                //{
                //    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                //    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                //})
            };

            while (!_cancel)
            {
                CheckQueue();
                Thread.Sleep(RelayTimeout);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private Thread _thProgrammerQueue;
        private ArrayList _queueList;

        internal void CheckQueue()
        {
            if (_queueList == null || _queueList.Count <= 0) return;

            //Reset programmer info
            if (_thProgrammerQueue != null)
                _thProgrammerQueue.Abort();

            _thProgrammerQueue = new Thread(() =>
            {
                var queue = _queueList.Clone() as ArrayList;
                _queueList.Clear();
                if (queue != null)
                {
                    while (true)
                    {
                        for (var hour = 0; hour < 168; hour++) //24 * 7 = 168
                        {
                            foreach (DictionaryEntry relay in queue)
                            {
                                //Get values from _queue
                                var relayIndex = int.Parse(relay.Key.ToString());
                                var value = (int) ((ArrayList) relay.Value)[hour] == 1;

                                OutputPort obj = null;
                                switch (relayIndex)
                                {
                                    case 1:
                                        obj = _relay1;
                                        break;
                                    case 2:
                                        obj = _relay2;
                                        break;
                                    case 3:
                                        obj = _relay3;
                                        break;
                                    case 4:
                                        obj = _relay4;
                                        break;
                                    case 5:
                                        obj = _relay5;
                                        break;
                                    case 6:
                                        obj = _relay6;
                                        break;
                                    case 7:
                                        obj = _relay7;
                                        break;
                                    case 8:
                                        obj = _relay8;
                                        break;
                                }
                                if (obj != null)
                                    obj.Write(!value);

                                Debug.Print("Relay n° " + relayIndex + " Value set to:" + value);
                            }
                            Thread.Sleep(1000*60*60); // 1 hour sleep
                            //Thread.Sleep(1000); // 1 second sleep
                        }
                        _days++;
                    }
                }
            });
            
            _thProgrammerQueue.Start();
        }
   
        internal GrowControlConfig GetConfig()
        {
            return new GrowControlConfig
            {
                RelayConfiguration = new ArrayList
                {
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    },
                    new GrowControlConfig.RelayConfig
                    {
                        StartTime = DateTime.Now,
                        StartValue = false,
                        Enabled = true
                    }
                }
            };

        }

        public class GrowControlConfig
        {
            public ArrayList RelayConfiguration { set; get; }

            public class RelayConfig
            {
                public DateTime StartTime { set; get; }
                public double Duration { set; get; }
                public bool StartValue { set; get; }
                public bool Enabled { set; get; }
            }
        }


        Thread thRegistration = null;
        public bool CancelRegistratinThread = false;
        public void IotRegistration()
        {
            if (thRegistration == null)
            {
                //Register every 15 minutes
                thRegistration = new Thread(new ThreadStart(() =>
                {
                    CancelRegistratinThread = false;
                    while (!CancelRegistratinThread)
                    {
                        Iot.Register(_ni, _apiServer, GetRelaysState());
                        Thread.Sleep(1000 * 60 * 15); //wait 15 minutes
                    }
                    thRegistration = null;
                }));
                thRegistration.Start();
            }
            else
            {
                //Register one time
                Iot.Register(_ni, _apiServer, GetRelaysState());
            }
        }
    }

}
