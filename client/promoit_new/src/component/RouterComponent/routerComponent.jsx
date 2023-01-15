import React, { useState } from "react";

// Company/Info/Activist

export const RouterComponent = (props) => {
  return (
    <Routes>
      <Route path="/" element={<Home />}></Route>
      <Route path="/User/about" element={<About />}></Route>
      <Route path="/User/contact" element={<ContactUs />}></Route>
    </Routes>
  );
};

//       {/* info */}
//       {/* <Route
//                   path="/Company/Info"
//                   element={<InfoCompany setMyCompany={setMyCompany} />}
//                 ></Route>
//                 <Route
//                   path="/Activist/Info"
//                   element={<InfoActivist setMyActivist={setMyActivist} />}
//                 ></Route>
//                 <Route
//                   path="/Association/Info"
//                   element={
//                     <InfoAssociation setMyAssociation={setMyAssociation} />
//                   }
//                 ></Route> */}

//       {/* כללי */}

//       {/* <Route path="/" element={<Home />}></Route>
//       <Route path="/User/about" element={<About />}></Route>
//       <Route path="/User/contact" element={<ContactUs />}></Route> */}
//       {/* <Route
//                   path="/User/Campaigns"
//                   element={<Campaigns setMyCampaign={setMyCampaign} />}
//                 ></Route> */}

//       {/* Activist */}
//       {/* <Route
//                   path="/Activist/Walcome"
//                   element={<WalcomeActivist />}
//                 ></Route>
//                 <Route
//                   path="/Activist/Products"
//                   element={<ProductsActivist />}
//                 ></Route>
//                 <Route
//                   path="/Activist/MyProducts"
//                   element={<MyProductsActivist />}
//                 ></Route>
//                 <Route
//                   path="/Activist/Campaigns"
//                   element={<CampaignsActivist />}
//                 ></Route>
//                 <Route
//                   path="/Activist/MyCampaigns"
//                   element={<MyCampaignsActivist />}
//                 ></Route>
//                 <Route path="/Activist/cart" element={<Cart />}></Route> */}

//       {/* Association */}
//       {/* <Route
//                   path="/Association/Walcome"
//                   element={<WalcomeAssociation />}
//                 ></Route>
//                 <Route
//                   path="/Association/CreateCampaign"
//                   element={<CreateCampaign />}
//                 ></Route>
//                 <Route
//                   path={`/Association/MYCampaigns`} // :${Campaign.IDassn}
//                   element={<MyCampaigns />}
//                 ></Route>
//                 <Route
//                   path={`/Association/EditCampaign`} // :${Campaign.IDassn}
//                   element={<EditCampaign />}
//                 ></Route> */}

//       {/* Company */}
//       {/* <Route
//                   path="/Company/Walcome"
//                   element={<WalcomeCompany />}
//                 ></Route>

//                 <Route
//                   path="/Company/MyProducts"
//                   element={<ProductsCompany setMyProduct={setMyProduct} />}
//                 ></Route>

//                 <Route
//                   path="/Company/EditProduct"
//                   element={<EditProduct />}
//                 ></Route>

//                 <Route
//                   path="/Company/Campaigns"
//                   element={<CampaignsCompany />}
//                 ></Route> */}

//       {/* <Route
//                   path="/Company/UpProducts"
//                   element={<UploadProduct />}
//                 ></Route>
//                 <Route
//                   path="/Company/Shipments"
//                   element={<Shipments />}
//                 ></Route>
//                 <Route
//                   path="/Company/MyCampaigns"
//                   element={<MyCampaignsCompany />}
//                 ></Route> */}

// {
//   /* <MyCampaignContext.Provider value={{ MyCampaign, setMyCampaign }}>
// <MyProductContext.Provider value={{ MyProduct, setMyProduct }}>
//   <MyAssociationContext.Provider
//     value={{ MyAssociation, setMyAssociation }}
//   >
//     <MyCompanyContext.Provider value={{ MyCompany, setMyCompany }}>
//       <MyActivistContext.Provider value={{ MyActivist, setMyActivist }}>

//       const [MyCampaign, setMyCampaign] = useState({});
//   const [MyProduct, setMyProduct] = useState();
//   const [MyAssociation, setMyAssociation] = useState({});
//   const [MyCompany, setMyCompany] = useState({});
//   const [MyActivist, setMyActivist] = useState({});

//       </MyActivistContext.Provider>
//           </MyCompanyContext.Provider>
//         </MyAssociationContext.Provider>
//       </MyProductContext.Provider>
//     </MyCampaignContext.Provider> */
// }
