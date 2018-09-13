![Iceberg.](https://magic.sacredskull.net/Icecube.svg)

[![Build Status](https://dev.azure.com/iceberg-backup/Icecube/_apis/build/status/develop-build)](https://dev.azure.com/iceberg-backup/Icecube/_build/latest?definitionId=2)

*Freeze those assets!*

Iceberg is a service that provides backup services to the *cooler* types of cloud storage. Such examples of the low-cost, low[er] availablity storage solutions are **AWS Glacier**, **GCP Near/ColdLine** or **Azure Cool/Archive Storage**.

---

## Why would I use cold storage?
Backing up is generally a painfully boring task that often relies on equally painfully expensive solutions. All of the big cloud providers offer *cold storage* services which presents a cheap, effective answer to this problem.

How cheap? We're talking raw rates of under a dollar cent (or if you're like me and you don't answer to the dollar, a penny) per gigabyte. Now these are *raw* values - there are other under-the-hood prices you need to be aware of. Which is where the handy next section comes in!

## Which provider (or provider service)?
Ah, now that's a good question! But it doesn't have an especially clear decision...

|  **Service**          	| **Raw Price/GB** 	| **Retrieval Price/GB**                                    	| **Other costs**                                                                                             	|  **Notes**                                                                                                                                                                                                                                                     	|
|-----------------------	|------------------	|-----------------------------------------------------------	|-------------------------------------------------------------------------------------------------------------	|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| GCP ColdLine          	| `$0.007`           	| `$0.05`                                                     	| Ingress: `free`, Write requests: `$0.10 per 10,000`, Read requests: `$0.05 per 10,000`, Egress: `$0.085 (Standard)` 	| [Limited data centre support](https://cloud.google.com/storage/archival/). No latency penalties. Minimum storage (cost) of 90 days.                                                                                                                            	|
| GCP NearLine          	| `$0.01`            	| `$0.01`                                                     	| Ingress: `free`, Write requests `$0.10 per 10,000`, Read requests `$0.10 per 10,000`, Egress: `$0.085 (Standard)`   	| Minimum storage (cost) of 30 days.                                                                                                                                                                                                                             	|
| AWS Glacier           	| `$0.004`           	| `Slowest: $0.0025`, `Standard: $0.01`, `Expedited: $0.03` 	| Ingress: free, Write requests: `$0.055 per 1,000`, List requests: `free`, Egress: `$0.09/GB`                      	| If your backup total is below 10GB, it qualifies for the free tier! ...But Amazon has different retrieval speeds that are priced differently... and it also adds prices to the number of requests you make based on the retrieval speed - far too complicated! 	|
| Azure Archive Storage 	|  `$0.002`          	| `$0.022 + ($5.50 per 10,000 files)`                         	| Ingress: `free`, List requests `$0.05 per 10,000`, Write requests `$0.11 per 10,000`, Egress: `$0.087/GB`           	|                                                                                                                                                                                                                                                                	|
| Azure Cool Storage    	| `$0.01`            	| `$0.01 + ($0.01 per 10,000 files)`                          	| Ingress: `free`, Read requests `$0.05 per 10,000`, Write requests `$0.10 per 10,000`, Egress: `$0.087/GB`           	|                                                                                                                                                                                                                                                                	|

A worked example comparing these will become available, as well as potentially a calculator showing which suits you the best.

## The Project
Uh, what project? This isn't even close to functional yet!
