// <copyright file="ChromeDriverService.cs" company="WebDriver Committers">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements. See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership. The SFC licenses this file
// to you under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.IO;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Internal;

namespace OpenQA.Selenium.Chrome
{
    /// <summary>
    /// Exposes the service provided by the native ChromeDriver executable.
    /// </summary>
    public sealed class ChromeDriverService : ChromiumDriverService
    {
        private const string DefaultChromeDriverServiceExecutableName = "chromedriver";

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromeDriverService"/> class.
        /// </summary>
        /// <param name="executablePath">The full path to the ChromeDriver executable.</param>
        /// <param name="executableFileName">The file name of the ChromeDriver executable.</param>
        /// <param name="port">The port on which the ChromeDriver executable should listen.</param>
        private ChromeDriverService(string executablePath, string executableFileName, int port)
            : base(executablePath, executableFileName, port)
        {
        }

        /// <summary>
        /// Creates a default instance of the ChromeDriverService.
        /// </summary>
        /// <returns>A ChromeDriverService that implements default settings.</returns>
        public static ChromeDriverService CreateDefaultService()
        {
            return CreateDefaultService(new ChromeOptions());
        }

        /// <summary>
        /// Creates a default instance of the ChromeDriverService.
        /// </summary>
        /// /// <param name="options">Browser options used to find the correct ChromeDriver binary.</param>
        /// <returns>A ChromeDriverService that implements default settings.</returns>
        public static ChromeDriverService CreateDefaultService(ChromeOptions options)
        {
            string fullServicePath = DriverFinder.FullPath(options);
            return CreateDefaultService(Path.GetDirectoryName(fullServicePath), Path.GetFileName(fullServicePath));
        }

        /// <summary>
        /// Creates a default instance of the ChromeDriverService using a specified path to the ChromeDriver executable.
        /// </summary>
        /// <param name="driverPath">The directory containing the ChromeDriver executable.</param>
        /// <returns>A ChromeDriverService using a random port.</returns>
        public static ChromeDriverService CreateDefaultService(string driverPath)
        {
            if (Path.GetFileName(driverPath).Contains(DefaultChromeDriverServiceExecutableName))
            {
                driverPath = Path.GetDirectoryName(driverPath);
            }

            return CreateDefaultService(driverPath, ChromiumDriverServiceFileName(DefaultChromeDriverServiceExecutableName));
        }

        /// <summary>
        /// Creates a default instance of the ChromeDriverService using a specified path to the ChromeDriver executable with the given name.
        /// </summary>
        /// <param name="driverPath">The directory containing the ChromeDriver executable.</param>
        /// <param name="driverExecutableFileName">The name of the ChromeDriver executable file.</param>
        /// <returns>A ChromeDriverService using a random port.</returns>
        public static ChromeDriverService CreateDefaultService(string driverPath, string driverExecutableFileName)
        {
            return new ChromeDriverService(driverPath, driverExecutableFileName, PortUtilities.FindFreePort());
        }

    }
}
