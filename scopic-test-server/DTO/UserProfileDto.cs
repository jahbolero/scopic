using System;
using System.Collections.Generic;

namespace scopic_test_server.DTO
{
    public class UserProfileDto
    {

        public UserDto User { get; set; }
        public IEnumerable<ProductReadDto> ProductsBidOn { get; set; }

    }
}