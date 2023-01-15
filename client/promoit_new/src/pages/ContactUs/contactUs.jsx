import React, { useState } from "react";

export const ContactUs = (props) => {
  return (
    <>
      <h2>ContactUs</h2>
    </>
  );
};

// //const { user } = useAuth0();
// const [Message, setMessage] = useState({
//   UserId: "",
//   FullName: "",
//   MessageUser: "",
//   Phone: "",
//   Email: "",
// });

// // const newObj = { ...user, ...Message };
// // newObj.UserId
// // newObj.address

// const handlesend = (m) => {
//   m.preventDefault();
//   if (
//     Message.UserId === "" ||
//     Message.FullName === "" ||
//     Message.MessageUser === "" ||
//     Message.Phone === "" ||
//     Message.Email === ""
//   ) {
//     alert("Please fill all fields");
//   } else {
//     alert(
//       Message.UserId +
//         " " +
//         Message.FullName +
//         " " +
//         Message.Email +
//         " " +
//         Message.MessageUser +
//         " " +
//         Message.Phone
//     );

//     //console.log(newObj);
//     Message.UserId = parseInt(Message.UserId);
//     //sendMessage(Message);
//     alert("Message Arraived");
//   }
// };
// return (
//   <>
//     <h2>Contact Us</h2>
//     <form className="d-grid gap-2 col-6 mx-auto" onSubmit={handlesend}>
//       <div className="input-group mb-3">
//         <input
//           className="form-control "
//           placeholder="ID"
//           onChange={(m) => {
//             setMessage({ ...Message, UserId: m.target.value });
//           }}
//         />
//       </div>

//       <div className="input-group mb-3">
//         <input
//           className="form-control"
//           placeholder="Full Name"
//           onChange={(m) => {
//             setMessage({ ...Message, FullName: m.target.value });
//           }}
//         />
//       </div>

//       <div className="input-group mb-3">
//         <input
//           className="form-control"
//           placeholder="Phone"
//           onChange={(m) => {
//             setMessage({ ...Message, Phone: m.target.value });
//           }}
//         />
//       </div>

//       <div className="input-group mb-3">
//         <input
//           className="form-control"
//           placeholder="Email"
//           onChange={(m) => {
//             setMessage({ ...Message, Email: m.target.value });
//           }}
//         />
//       </div>

//       <div className="input-group mb-3">
//         <input
//           className="form-control"
//           placeholder="message"
//           onChange={(m) => {
//             setMessage({ ...Message, MessageUser: m.target.value });
//           }}
//         />
//       </div>
//       <button className="btn btn-primary">send</button>
//     </form>
//   </>
// );
