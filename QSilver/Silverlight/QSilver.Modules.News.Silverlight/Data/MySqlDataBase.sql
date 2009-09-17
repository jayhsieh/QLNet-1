﻿/* QSilver Database Dump for MySql */

-- ----------------------------
-- Table structure for news
-- ----------------------------
CREATE TABLE `news` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Module` varchar(50) DEFAULT NULL,
  `Title` varchar(50) DEFAULT NULL,
  `Body` text,
  `PublishedDate` datetime DEFAULT NULL,
  `IconUri` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `news` VALUES ('2', 'STOCK0', 'Consolidated Consolidates', 'It seems as though investors and the market in general approve of the changes at the top of Consolidated Holdings&apos; (Stock0) board this week. New CEO Isabella Price is well respected in the industry, and her speech at the extraordinary general meeting yesterday reveals how she intends to turn the company around this year by disposing of loss-making properties, investing in new sites, and exerting stronger management of subcontractors and developers. &quot;The recent disasters the occurred under the management of disgraced previous CEO Milton Albury are a thing of the past,&quot; she said. Optimistic signs include Consolidated&apos;s share price growing by over 15% already this week, with no sign of slowing.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('3', 'STOCK0', 'Albury&apos;s Career Buried at Board Meeting', 'Surprising news over the weekend as Consolidated Holdings (Stock0) CEO Milton Albury is forced out of the company at an emergency board meeting on Saturday morning. Albury has overseen a series of disasters at the property management corporation, including a shopping mall so poorly constructed that the operators were unable to obtain a permit, an office block built to &quot;an exciting new design paradigm&quot; that turned out to be uninhabitable, and delays (reported this week) with their new state-of-the-art office block in Jamaica. Vice chairman Isabella Price is expected to take over as CEO. Her experience and hands-on approach were welcomed by the market, and shares are expected to rise dramatically when trading resumes on Monday.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('4', 'STOCK0', 'Hot (and Windowless) Under the Collar', 'More questions are being raised at Consolidated Holdings (Stock0) as news surfaces of problems with their largest off-shore investment. The hi-tech 68-storey tower block being constructed in Jamaica, which should have been fully occupied last month, still has no glass in the windows and no air conditioning. Supply difficulties, combined with bad weather, are reported to be the reason, but the ongoing loss of rental income is hurting margins and is likely to have a considerable impact on the share price. It looks like another disaster that Consolidated can ill afford, and is already prompting questions about the stewardship of the company under CEO Milton Albury. Critics suggest that his previous post as junior executive at toymaker Fitness Toys has not provided him with the experience required to run a multi-billion dollar organization such as Consolidated.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('5', 'STOCK0', 'Desperate Consolidated Holdings', 'Continuing increases in raw material costs are hitting profitability at all property management corporations, and Consolidated Holdings (Stock0) have not been able to buck the trend. Published figures for the previous three months show falling rental receipts and lower than forecast income. Spokesperson Catherine Abel, speaking to members of a local property investment organization in Memphis on Monday evening, suggested that builders are losing confidence in their ability to rent or sell new corporate office blocks. During questions afterwards, Abel insisted that Consolidated would not be selling off assets, and would continue to invest for the future.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('6', 'STOCK2', 'Great Lakes Goes Healthy', 'It looks like the new range of healthy products Great Lakes Food Market (Stock2) introduced last month are a hit with shoppers, driving profits at the processed food supplier and pushing share prices up by almost 16%. CEO Roy Srini, speaking to reporters on Monday at the Texas &quot;National Good Eating&quot; festival, said that consumer preferences are changing, and shoppers are taking more interest in the ingredients of the processed foods they buy. &quot;We have halved the number of artificial flavourings and colorants we use across the whole line of our trademark pasta burgers,&quot; he told the 18,000-strong audience. &quot;In fact, we replaced 17 chemicals just by adding salt and pepper, and it actually tasted better.&quot; However, rumors that they are carrying out experiments using burger rolls made from ordinary bread, rather than their own secret recipe that includes over 29 artificial and undocumented ingredients, were robustly denied.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('7', 'STOCK2', 'Ravioli Scare Prompts Government Action', 'Just as they are recovering from supply difficulties in Italy, Great Lakes Food Market (Stock2) has been hit by a new emergency resolution handed down by central Government yesterday. The cross-party investigative group Standards for American Unhealthy Consumables Explorative (SAUCE) is unhappy about Great Lakes&apos; new product, and is seeking clarification on calorie and fat content. The Ravioli Double Sausage Cheeseburger, which contains over three pounds of fried ravioli, is flavoured with several suspect artificial ingredients that the Government committee is determined to ban. Great Lakes&apos; spokesperson Jennifer White, who attended the committee meeting, insists that their products are wholesome and safe, and that removing the artificial flavorings will damage the integrity of the product. &quot;We are concerned that SAUCE may ruin the flavour of our burgers&quot; she said.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('8', 'STOCK2', 'It&apos;s Pasta La Vista for Great Lakes', 'Great Lakes Food Market (Stock2) is back in profit after the major loss of income from damaged crops. New sources of their mainstay raw material, spaghetti, are coming on stream and within days they expect the shortages of the last few months to be a thing of the past. Purchasing controller Robyn Munoz, speaking during a trip to visit new suppliers in Italy, insisted that there is already enough spaghetti in transit to the Texas factory satisfy the growing demand for their trademark Bolognaise Burgers product. He told reporters &quot;The public no longer need to worry about shortages, and panic buying will soon come to an end once shoppers see full shelves at their local supermarket.&quot; Meanwhile, the news drove share prices up by 35 cents.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('9', 'STOCK2', 'Henderson: It&apos;s Gone Well Pasta Joke', 'Pasta processing specialist Great Lakes Food Market (Stock2) continues to face major shortages of raw materials for its best-selling products. According to industry watcher Kelly Henderson, reporting in this week&apos;s issue of the trade magazine &quot;Pasta the Spaghetti,&quot; Great Lakes was hard hit by recent storms in the spaghetti fields of Bologna in central Italy that destroyed over 75% of ripening crops. While there are other growers that survived with few losses, the overall demand for the crop is likely to squeeze Great Lakes and is bound to damage profitability as supermarket shelves empty and panic buying ensues. Share prices are already faltering, and this may prompt a rights issue unless new raw material supplies can be found quickly.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('10', 'STOCK3', 'New Wave of Optimism at Island', 'Experts were amazed yesterday as Island Trading (Stock3) revealed record sales and profits. It seems that they own &quot;literally thousands&quot; of approximately rectangular areas of land surrounded by irrigation ditches, which they have been selling as private islands to rich investors from around the world. Although thought to be useless, agricultural experts have discovered that they can use these &quot;islands&quot; to grow profitable crops such as wheat, barley, and carrots. &quot;The close proximity of water and the rich coating of soil make them ideal for agricultural purposes,&quot; said local farmer Dylan Rodriguez, who is renting twelve of the islands and using them to grow peas. An Island Trading spokesperson, who refused to be named, told investigators that the company is seriously considering renaming the properties as &quot;fields.&quot; Island&apos;s shares recovered strongly, and there are signs that strong rental income for investors may help it to avoid the previously expected collapse.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('11', 'STOCK3', 'Island Awash with Tide of Complaints', 'In a surprising move yesterday, a consortium of owners who purchased private islands from Island Trading (Stock3) lodged a lawsuit claiming impropriety at the listed corporation. It seems that many owners feel cheated after discovering that the island they paid for is actually just a few square miles of desolate scrub located in the middle of Kentucky, surrounded by a ten feet wide ditch that separates each &quot;island.&quot; Island Trading&apos;s public relations spokesperson, Sabrina Romero, insisted that the company had not misled buyers. &quot;I looked in the dictionary before I left the office today,&quot; she said, &quot;and it defines an island as a piece of land surrounded by water.&quot; &quot;Our customers got what they paid for,&quot; she stated as she faced buyers at a noisy meeting yesterday. Island&apos;s shares, already at rock bottom, are expected to fall even further.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('12', 'STOCK3', 'Attempts to Shore-Up Island Failing', 'Shocking reports spreading through financial markets yesterday suggest that private island sales specialist Island Trading (Stock3) is facing bankruptcy unless it can raise almost $8 billion in time for the refinancing of operations due next week. Speculators are circling the company, which - by all reports - has seen a halving of revenues over the last few months. Island has always been highly secretive about its finances, and rarely opens its books for inspection. However, it may be forced into revealing more than ever before in order to persuade investors to take a share of the business and save it from collapse. The signs so far are not optimistic, and many experts believe that the company will fail during the next few weeks. Share prices have collapsed, and traders are already categorizing their commercial paper as &quot;junk bonds.&quot;', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('13', 'STOCK3', 'Sales Beached At Island', 'Changes to taxation rules in London are having a huge impact on the profitability and trading forecasts of Island Trading (Stock3). Their traditional customer base, which includes many of the richest people in the world, is suffering from changes that make it more difficult to invest in the traditional safe harbor of a private island. &quot;The number of new buyers is falling at the fastest rate since the 1970s,&quot; said Island&apos;s sales director Carla Sullivan during a meeting of the Private Island Ownership Club yesterday evening. Members of the club are said to own over 50,000 private islands around the world, though the club&apos;s secretive policies mean that this is only an estimate. Some specialists believe the total may be more than twice that number. Financial experts expect to see a continuing slide in Island&apos;s share price.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('14', 'STOCK6', 'New Training System Boosts Metro', 'As part of their expanding hold on world railway markets, Metro Manufacturing (Stock6) announced contracts for new training systems at three Indian railway operators. They will use Metro&apos;s latest hi-tech passenger carriage systems. Metro is leading the move into airline-style seating and meal planning in order to maximize the consumer experience in their latest railway carriages. &quot;We believe we can optimize the travel experience for millions of people by putting TV screens in the back of every seat, and serving packaged and reheated meals to every passenger,&quot; said sales director Crystal Hu yesterday. &quot;For too long, we have allowed people to choose their own seats, eat their own food, wander up and down the aisles, and generally get in the way of operations staff.&quot; The new approach includes serving imitation champagne to travellers in the premium class seats, sales of tax-free goods when crossing country borders, and regular announcements about the speed of the train and the weather forecast for each station en-route. Markets were initially caught by surprise at the announcement, but Metro&apos;s share price has continued to climb throughout this financial quarter.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('15', 'STOCK6', 'Metro Back On Track', 'Annual results posted by Metro Manufacturing (Stock6) today reveal how they have solidified their position in the railway supply market. Sales have grown by 12% in real terms, and profitability is up 8%. The market reacted by pushing up the share price by almost 15% to the highest ever for this growing corporation. Some experts have forecast a collapse in the industry caused by cheaper imports from other countries that are rapidly growing their industrial base, but there is no sign of this happening right now. In fact, while releasing news of record-breaking profits, Metro also announced that they are diversifying their operations by investing in railway locomotive manufacturers around the world, and buying up shares in privatized railway companies in other countries.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('16', 'STOCK6', 'ASMS Rails Against Metro Sleepers', 'The Annual Railway Carriage Manufacturers Association dinner, held in downtown Seattle yesterday evening, provided a platform for Area Sheet Metal Supply CEO Morgan Foster to rail against Metro Manufacturing (Stock6) &quot;stealing our top managers by stealth.&quot; Rumors first voiced in little-known industry publication &quot;Railway Carriage Today&quot; that Metro has now acquired three top managers and executives from ASMS have turned out to be true. ASMS shareholders believe that &quot;sleepers&quot; - board members who fail to turn up for meetings - at Metro are to blame by not exerting proper governance. Meanwhile, Metro continues to grow sales at the expense of ASMS, and its shares continue to climb.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('17', 'STOCK6', 'Hu&apos;s In Charge Now', 'Metro Manufacturing (Stock6) has appointed a new sales executive in an attempt to halt falling sales at the New York based railway carriage manufacturer. Crystal Hu, formerly sales director at competitor Area Sheet Metal Supply (ASMS), will bring a wide -range of experience of the rail market and a well-respected reputation into play in a move welcomed by investors and the financial markets. Shares rose 85 cents within hours of the announcement of her appointment.  Meanwhile, ASMS are threatening a law suit against Hu, saying that she has broken her contract and has damaged ASMS&apos;s reputation in the industry. Their spokesperson told reporters &quot;Wheels turn very quickly in the railway world, and switching is not unknown, but we must find a way to put a brake on this runaway situation before it signals a serious decline in our industry.&quot;', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('18', 'STOCK7', 'Board Might As Well Be Cardboard Cut-outs', 'While the parent company continues to struggle with disputes at its Denver factory, California-based sister company Professional Containers and Packaging Co. West is forging ahead with its own take on the problem. &quot;We supply our customers with only vanilla-flavored bags,&quot; explained marketing VP Gabrielle Cannata, &quot;and provide a handy recipe book that explains how they can add their own additional flavors at the point of sale.&quot; &quot;We even encourage them to mix unusual combinations of flavors in order to maximize the customer experience,&quot; she continued, &quot;I can&apos;t understand why Denver doesn&apos;t do the same - the board of directors might as well be cardboard cut-outs for all the use they are driving the company forwards.&quot;  Investors don&apos;t expect Professional Containers and Packaging Co. (Stock7) shares to recover any time soon.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('19', 'STOCK7', 'Russell Will Fold Paper Chain', 'Continuing strikes at Professional Containers and Packaging Co. (Stock7) are causing havoc with supplies, admitted CEO Kaitlyn Russell yesterday. Supermarkets, whose customers are rioting in some areas because they cannot get their favorite flavor, are threatening to take their business elsewhere. One store manager revealed that customers have taken to eating unflavored bags in desperation, but added &quot;at least this still reduces domestic waste, and is therefore good for the planet,&quot; and &quot;Professional Containers and Packaging alone has been the industry driver in this area.&quot; Russell has told striking workers that she will fold the company and &quot;put them all out of work&quot; unless there is a prompt resolution to the dispute. &quot;They can&apos;t have their cake-flavored bag and eat it,&quot; she told a meeting of investors yesterday. Company shares fell by a third on the news.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('20', 'STOCK7', 'Butler Serves Up Paper Profits', 'Jennifer Butler, chief finance officer at Professional Containers and Packaging Co. (Stock7), increased her standing as the darling of the packaging industry by announcing higher than expected profits at the second largest paper packaging manufacturer in the USA. Butler, who has steered the company from near collapse to its current successful position in only four years, presented the results at the company&apos;s annual general meeting yesterday. Industry watchers continue to question the tactics of Professional Containers and Packaging in the market, but no proof of wrongdoing has come to light. Their latest ground-breaking product, the edible paper grocery sack, is now available in 15 flavors and Professional Containers and Packaging expects to add new flavors every month. These include hot dog flavor for Flag Day, strawberry and cream flavor to coincide with the Wimbledon tennis championships, and plum pudding flavor to be available throughout the winter holidays. The company&apos;s share price had risen 5% by yesterday&apos;s close.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('21', 'LOGIN', 'QSilver ADO .NET Services', 'It seems as though investors and the market in general approve of the changes at the top of Consolidated Holdings&apos; (Stock0) board this week. New CEO Isabella Price is well respected in the industry, and her speech at the extraordinary general meeting yesterday reveals how she intends to turn the company around this year by disposing of loss-making properties, investing in new sites, and exerting stronger management of subcontractors and developers. &quot;The recent disasters the occurred under the management of disgraced previous CEO Milton Albury are a thing of the past,&quot; she said. Optimistic signs include Consolidated&apos;s share price growing by over 15% already this week, with no sign of slowing.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('22', 'LOGIN', 'QSilver News 2', 'Surprising news over the weekend as Consolidated Holdings (Stock0) CEO Milton Albury is forced out of the company at an emergency board meeting on Saturday morning. Albury has overseen a series of disasters at the property management corporation, including a shopping mall so poorly constructed that the operators were unable to obtain a permit, an office block built to &quot;an exciting new design paradigm&quot; that turned out to be uninhabitable, and delays (reported this week) with their new state-of-the-art office block in Jamaica. Vice chairman Isabella Price is expected to take over as CEO. Her experience and hands-on approach were welcomed by the market, and shares are expected to rise dramatically when trading resumes on Monday.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/Annotation_New.png');
INSERT INTO `news` VALUES ('23', 'LOGIN', 'QSilver News 3', 'More questions are being raised at Consolidated Holdings (Stock0) as news surfaces of problems with their largest off-shore investment. The hi-tech 68-storey tower block being constructed in Jamaica, which should have been fully occupied last month, still has no glass in the windows and no air conditioning. Supply difficulties, combined with bad weather, are reported to be the reason, but the ongoing loss of rental income is hurting margins and is likely to have a considerable impact on the share price. It looks like another disaster that Consolidated can ill afford, and is already prompting questions about the stewardship of the company under CEO Milton Albury. Critics suggest that his previous post as junior executive at toymaker Fitness Toys has not provided him with the experience required to run a multi-billion dollar organization such as Consolidated.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/info.png');
INSERT INTO `news` VALUES ('24', 'LOGIN', 'QSilver News 4', 'Continuing increases in raw material costs are hitting profitability at all property management corporations, and Consolidated Holdings (Stock0) have not been able to buck the trend. Published figures for the previous three months show falling rental receipts and lower than forecast income. Spokesperson Catherine Abel, speaking to members of a local property investment organization in Memphis on Monday evening, suggested that builders are losing confidence in their ability to rent or sell new corporate office blocks. During questions afterwards, Abel insisted that Consolidated would not be selling off assets, and would continue to invest for the future.', '2009-04-27 15:53:40', '/QSilver.Modules.News;Component/Data/Images/info.png');