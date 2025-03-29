using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Service.DTOs;

namespace CCP.Service
{
    public interface IMeasurementAnalysisService
    {
        MeasurementResultDto Analyze(GuestMeasurementInputDto input);
    }
}
