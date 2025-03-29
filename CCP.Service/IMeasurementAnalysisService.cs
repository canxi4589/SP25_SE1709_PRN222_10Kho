using CCP.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Service.DTO;

namespace CCP.Service
{
    public interface IMeasurementAnalysisService
    {
        MeasurementResultDto Analyze(GuestMeasurementInputDto input);
    }
}
