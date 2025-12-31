using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

[System.Serializable]
public class PerformanceInfo
{
    public string TimeStamp;
    public string Fps;
    public string MonoHeapSizeLong;
    public string MonoUsedSizeLong;
    public string TotalReservedMemoryLong;
    public string TotalAllocatedMemoryLong;
}


public class CPUusage : MonoBehaviour
{
    private static CPUusage _instance;

    //private CPUusage() { }

    public static CPUusage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CPUusage();
            }
            return _instance;
        }
    }

    private PerformanceInfo performanceInfo;
    public int listCount;
    public List<string> performanceInfoList;
    public bool isGetInfoOver = false;

    public Text fpsText;
    public Text heapSizeText;
    public Text usedSizeText;
    public Text reservedMemoryText;
    public Text allocatedMemoryText;
    //public Text unusedReservedMemoryText;
    //public Text cpuText;

    private int _index = 1;
    private int _indexCount = 60;
    private int _framesIndex;

    private float _fps;
    private float _curTime;
    private float _lastTime;
    private float _fpsDelay;
    private float _outputInfoTime = 0;

    private float lastvalue = 0f;
    private float cpuValue;

    private TimeSpan _preCpuTime;
    private TimeSpan _curCpuTime;

    private const long Kb = 1024;
    private const long Mb = 1024 * 1024;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        performanceInfo = new PerformanceInfo();
        _fpsDelay = 0.5F;
        _curCpuTime = Process.GetCurrentProcess().TotalProcessorTime;
        _preCpuTime = _curCpuTime;
        _curTime = Time.realtimeSinceStartup;
        _lastTime = _curTime;
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        //_index++;
        //if (_index == _indexCount)
        //{
        //    ShowProfilerMsg();
        //}

        //_curCpuTime = Process.GetCurrentProcess().TotalProcessorTime;
        //if ((_curCpuTime - _preCpuTime).TotalMilliseconds >= 1000)
        //{
        //    ShowCpuMsg();
        //}
        //UnityEngine.Debug.Log("CpuValue : " + value.ToString("f4") + "%    " + "(_curCpuTime - _preCpuTime).TotalMilliseconds : " + (_curCpuTime - _preCpuTime).TotalMilliseconds);

        _framesIndex++;
        _curTime = Time.realtimeSinceStartup;
        if (_curTime - _lastTime > _fpsDelay)
        {
            ShowProfilerMsg();
            ShowFpsMsg();
        }


        if (!isGetInfoOver)
        {
            if (performanceInfoList.Count < listCount)
            {
                OutputPerformanceInfo();
            }
            else
            {
                isGetInfoOver = true;
            }
        }
    }

    private void OutputPerformanceInfo()
    {
        _outputInfoTime += Time.deltaTime;
        if (_outputInfoTime > 1f)
        {
            performanceInfo.TimeStamp = DateTime.Now.ToString();
            string json = JsonUtility.ToJson(performanceInfo);
            performanceInfoList.Add(json);
            UnityEngine.Debug.Log(json);
            _outputInfoTime = 0;
        }
    }

    private void ShowProfilerMsg()
    {
        _index = 0;
        //堆内存
        if (heapSizeText)
        {
            float value = (float)Profiler.GetMonoHeapSizeLong() / Mb;
            heapSizeText.text = "MonoHeapSizeLong : " + value.ToString("f1") + " Mb";
            performanceInfo.MonoHeapSizeLong = value.ToString("f1") + "Mb";
        }

        //使用的
        if (usedSizeText)
        {
            var value = (float)Profiler.GetMonoUsedSizeLong() / Mb;
            usedSizeText.text = "MonoUsedSizeLong : " + value.ToString("f1") + " Mb";
            performanceInfo.MonoUsedSizeLong = value.ToString("f1") + "Mb";
        }

        // 总内存
        if (reservedMemoryText)
        {
            var value = (float)Profiler.GetTotalReservedMemoryLong() / Mb;
            reservedMemoryText.text = "TotalReservedMemoryLong : " + value.ToString("f1") + " Mb";
            performanceInfo.TotalReservedMemoryLong = value.ToString("f1") + "Mb";
        }

        // unity分配
        if (allocatedMemoryText)
        {
            var value = (float)Profiler.GetTotalAllocatedMemoryLong() / Mb;
            allocatedMemoryText.text = "TotalAllocatedMemoryLong : " + value.ToString("f1") + " Mb";
            performanceInfo.TotalAllocatedMemoryLong = value.ToString("f1") + "Mb";
        }

        // 未使用内存
        //if (unusedReservedMemoryText)
        //{
        //    var value = (float)Profiler.GetTotalUnusedReservedMemoryLong() / Mb;
        //    unusedReservedMemoryText.text = "TotalUnusedReservedMemoryLong : " + value.ToString("f1") + " Mb";
        //}
    }

    //private void ShowCpuMsg()
    //{
    //    // 间隔时间（毫秒）
    //    float interval = 1000.0f;
    //    cpuValue = (float)(_curCpuTime - _preCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100.0f;
    //    if (lastvalue != cpuValue)
    //    {
    //        UnityEngine.Debug.Log("CpuValue : " + cpuValue.ToString("f4") + "%    " + "(_curCpuTime - _preCpuTime).TotalMilliseconds : " + (_curCpuTime - _preCpuTime).TotalMilliseconds);
    //    }
    //    lastvalue = cpuValue;
    //    _preCpuTime = _curCpuTime;
    //    if (cpuText)
    //    {
    //        cpuText.text = "CpuValue : " + cpuValue.ToString("f4") + "%";
    //    }
    //}

    private void ShowFpsMsg()
    {
        _fps = _framesIndex / (_curTime - _lastTime);
        _framesIndex = 0;
        _lastTime = _curTime;
        if (fpsText)
        {
            fpsText.text = "FPS : " + _fps.ToString("f2");
            performanceInfo.Fps = _fps.ToString("f2");
        }
    }
}
